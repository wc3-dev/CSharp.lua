using System;
using System.Collections.Generic;
using System.Linq;
// stand-ins for Moonwell's handle wrappers and reaction classes
public abstract class Handle { public int Id; static Dictionary<int, Handle> known = new(); protected Handle(int id) { Id = id; known[id] = this; } public static T Take<T>(int id) where T : Handle => (T)known[id]; }
public class Unit : Handle { public string Name; public Unit(int id, string n) : base(id) { Name = n; } public virtual void OnHit(int dmg) { } public virtual void OnDeath() { } public override string ToString() => "Unit:" + Name; }
public class Item : Handle { public Item(int id) : base(id) { } }
public class Hero : Unit { public int Level = 1; public Hero(int id, string n) : base(id, n) { } public override void OnHit(int dmg) { Console.WriteLine("hero hit " + dmg); } }
public class Paladin : Hero { public Paladin(int id) : base(id, "pal") { } public override void OnDeath() { Console.WriteLine("pal died"); base.OnDeath(); } public override void OnHit(int dmg) { base.OnHit(dmg * 2); } }
[AttributeUsage(AttributeTargets.Class)] public class TagAttribute : Attribute { public string Name; public TagAttribute(string n) { Name = n; } }
[Tag("mapunit")] public class MyUnit : Unit { public MyUnit() : base(99, "my") { } }
class Program {
  static void T1() { var u = new Unit(1, "a"); var h = new Hero(2, "h"); Unit x = new Paladin(3); x.OnHit(5); x.OnDeath(); h.OnHit(1); u.OnHit(1); }
  static void T2() { var d = new Dictionary<Unit, int>(); var a = new Unit(10, "a"); var b = new Unit(11, "b"); d[a] = 1; d[b] = 2; d[a] += 5; Console.WriteLine(d[a] + " " + d[b] + " " + d.Count + " " + d.ContainsKey(new Unit(12, "c"))); }
  static void T3() { var l = new List<Unit>(); l.Add(new Unit(20, "x")); l.Add(new Hero(21, "y")); Console.WriteLine(l.Count + " " + l.OfType<Hero>().Count() + " " + l.Contains(l[0]) + " " + l.IndexOf(l[1])); }
  static void T4() { var hs = new HashSet<Handle>(); var a = new Unit(30, "a"); hs.Add(a); hs.Add(a); hs.Add(new Item(31)); Console.WriteLine(hs.Count + " " + hs.Contains(a)); }
  static void T5() { var u = new Unit(40, "q"); Console.WriteLine(Handle.Take<Unit>(40).Name + " " + ReferenceEquals(Handle.Take<Unit>(40), u)); }
  static void T6() { var m = new MyUnit(); var attrs = typeof(MyUnit).GetCustomAttributes(typeof(TagAttribute), false); Console.WriteLine(attrs.Length); }
  static void T7() { Unit u = new Hero(50, "z"); Console.WriteLine((u is Hero) + " " + (u as Hero).Level + " " + u.GetType().Name + " " + (u.GetType() == typeof(Hero)) + " " + u); }
  static void T8() { var d = new Dictionary<Type, List<Unit>>(); d[typeof(Hero)] = new List<Unit>(); d[typeof(Hero)].Add(new Hero(60, "h")); Console.WriteLine(d[typeof(Hero)].Count + " " + d.ContainsKey(typeof(Unit))); }
  static void T9() { var u = new Unit(70, "k"); var h = new Hero(71, "h"); Console.WriteLine(OverridesHit(u) + " " + OverridesHit(h) + " " + OverridesHit(new Paladin(72))); }
  static bool OverridesHit(Unit u) { var m = u.GetType().GetMethod("OnHit"); return m.DeclaringType != typeof(Unit); }
  static void T10() { var us = new[] { new Unit(80, "b"), new Unit(81, "a") }; var s = us.OrderBy(x => x.Name).Select(x => x.Name); Console.WriteLine(string.Join(",", s)); }
  static void T11() { Action<Unit> cb = u => Console.WriteLine("cb " + u.Name); var l = new List<Action<Unit>> { cb, cb }; foreach (var c in l) c(new Unit(90, "w")); }
  static void T12() { var w = new Dictionary<Unit, Dictionary<string, int>>(); var u = new Unit(100, "u"); w[u] = new(); w[u]["k"] = 3; Console.WriteLine(w[u]["k"]); }
  static void Main() { H.R("T1", T1); H.R("T2", T2); H.R("T3", T3); H.R("T4", T4); H.R("T5", T5); H.R("T6", T6); H.R("T7", T7); H.R("T8", T8); H.R("T9", T9); H.R("T10", T10); H.R("T11", T11); H.R("T12", T12); }
}

static class H { public static void R(string n, System.Action a) { System.Console.WriteLine("# " + n); try { a(); } catch (System.Exception e) { string m; try { m = e.Message; } catch (System.Exception) { m = "?"; } System.Console.WriteLine("#! " + n + " " + m); System.Console.WriteLine(n + " EXC"); } } }
