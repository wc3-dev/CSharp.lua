using System;
using System.Collections.Generic;
using Alias = Box;

class Box {
  public object Inner;
}

class Crate : Box {
}

class Other {
}

interface IThing {
}

class Thing : IThing {
}

struct Spot {
  public int X;
}

class Shelf {
  public Box Box;
}

namespace Goods {
  class Sack {
  }
}

class Program {
  static string When(object o, bool yes) {
    switch (o) {
      case Box when yes: return "box";
      case Other when !yes: return "other";
      case Goods.Sack when yes: return "sack";
      case List<int> when yes: return "list";
      default: return "none";
    }
  }

  static string Aliased(object o, bool yes) {
    switch (o) {
      case Alias when yes: return "alias";
      default: return "none";
    }
  }

  static string OfType<T>(object o, bool yes) {
    switch (o) {
      case T when yes: return "T when";
      default: return "none";
    }
  }

  static string OfTypeAlone<T>(object o) {
    switch (o) {
      case T: return "T";
      default: return "none";
    }
  }

  static string Parenthesized(object o, bool yes) {
    switch (o) {
      case (Box or Other) when yes: return "box";
      default: return "none";
    }
  }

  static string CaseOr(object o) {
    switch (o) {
      case Box or Other: return "either";
      default: return "none";
    }
  }

  static string CaseNotAnd(object o) {
    switch (o) {
      case Box and not Crate: return "plain box";
      case not Other: return "not other";
      default: return "other";
    }
  }

  static string Member(object o) {
    switch (o) {
      case Box { Inner: Other }: return "holds other";
      case Box { Inner: Box or string }: return "holds box or text";
      default: return "none";
    }
  }

  static string Element(object first, int n) {
    switch ((first, n)) {
      case (Box, 1): return "box one";
      case (Other, _): return "other";
      default: return "none";
    }
  }

  static string Arm(object o) {
    return o switch {
      Box => "box",
      Other => "other",
      Goods.Sack => "sack",
      string => "text",
      _ => "none",
    };
  }

  static string ArmWhen(object o, bool yes) {
    return o switch {
      Box when yes => "box",
      string when !yes => "text",
      _ => "none",
    };
  }

  static string ArmElement(object first, int n) {
    return (first, n) switch {
      (Box, 1) => "box one",
      (Other, _) => "other",
      _ => "none",
    };
  }

  static string IsForms(object o) {
    var either = o is Box or Other;
    var parenthesized = o is (Box);
    var neither = o is not (Box or Other);
    return either + " " + parenthesized + " " + neither;
  }

  static string IsMember(object o) {
    return (o is Box { Inner: Other or string }).ToString();
  }

  static string Items(List<object> items) {
    return (items is [Box, ..]) + " " + (items is [_, Other]);
  }

  static string Constants(object o, int n, Kind kind) {
    var text = "";
    switch (n) {
      case 1 when o != null: text = "one"; break;
      default: text = "many"; break;
    }
    switch (kind) {
      case Kind.Small when n == 1: return text + " small";
      default: return text + " other";
    }
  }

  const int Limit = 3;
  const string Word = "word";

  static string Named(int n, string s, bool yes) {
    switch (n) {
      case Limit when yes: return s == Word ? "limit word" : "limit";
      default: break;
    }
    return s switch {
      Word when yes => "word",
      _ => "none",
    };
  }

  static string SameType(Box b, bool yes) {
    switch (b) {
      case Box when yes: return "box";
      default: return "none";
    }
  }

  static string SameTypeForms(Box b, string s) {
    var not = b is not Box;
    var notText = s is not string;
    var notNamed = b is not Box c;
    var arm = b switch {
      Box => "box",
      _ => "null",
    };
    return not + " " + notText + " " + notNamed + " " + arm;
  }

  static string SameMember(Shelf s) {
    return (s is { Box: Box }) + " " + (s is { Box: not Box });
  }

  static string ValueType(int n) {
    switch (n) {
      case int when n > 1: return "big";
      default: return "small";
    }
  }

  static string ValueArm(int n) {
    return n switch {
      int when n > 1 => "big",
      int => "small",
    };
  }

  static string Interfaces(object o) {
    switch (o) {
      case IThing when o != null: return "thing";
      case Spot when o != null: return "spot";
      default: return "none";
    }
  }

  static string InterfaceArm(object o) {
    return o switch {
      IThing => "thing",
      Spot => "spot",
      _ => "none",
    };
  }

  enum Kind { Small, Large }

  public static void Main() {
    var box = new Box();
    var crate = new Crate();
    var other = new Other();
    Console.WriteLine(When(box, true) + " " + When(box, false) + " " + When(crate, true) + " " + When(other, false) + " " + When(other, true));
    Console.WriteLine(When(new Goods.Sack(), true) + " " + When(new List<int>(), true) + " " + When(null, true));
    Console.WriteLine(Aliased(box, true) + " " + Aliased(other, true));
    Console.WriteLine(OfType<Box>(box, true) + " " + OfType<Box>(other, true) + " " + OfTypeAlone<Box>(crate) + " " + OfTypeAlone<Box>(other));
    Console.WriteLine(Parenthesized(box, true) + " " + Parenthesized(box, false));
    Console.WriteLine(CaseOr(box) + " " + CaseOr(other) + " " + CaseOr("a"));
    Console.WriteLine(CaseNotAnd(box) + " " + CaseNotAnd(crate) + " " + CaseNotAnd(other));
    Console.WriteLine(Member(new Box { Inner = other }) + " " + Member(new Box { Inner = box }) + " " + Member(new Box { Inner = "a" }) + " " + Member(box));
    Console.WriteLine(Element(box, 1) + " " + Element(box, 2) + " " + Element(other, 2));
    Console.WriteLine(Arm(box) + " " + Arm(crate) + " " + Arm(other) + " " + Arm(new Goods.Sack()) + " " + Arm("a") + " " + Arm(1));
    Console.WriteLine(ArmWhen(box, true) + " " + ArmWhen(box, false) + " " + ArmWhen("a", false) + " " + ArmWhen("a", true));
    Console.WriteLine(ArmElement(box, 1) + " " + ArmElement(box, 2) + " " + ArmElement(other, 2));
    Console.WriteLine(IsForms(box) + " | " + IsForms(other) + " | " + IsForms("a"));
    Console.WriteLine(IsMember(new Box { Inner = other }) + " " + IsMember(new Box { Inner = box }));
    Console.WriteLine(Items(new List<object> { box, other }) + " " + Items(new List<object> { other, box }));
    Console.WriteLine(Constants(box, 1, Kind.Small) + " " + Constants(box, 2, Kind.Large));
    Console.WriteLine(Named(3, "word", true) + " " + Named(3, "a", true) + " " + Named(3, "word", false) + " " + Named(1, "word", true));
    Console.WriteLine(SameType(box, true) + " " + SameType(box, false) + " " + SameType(null, true));
    Console.WriteLine(SameTypeForms(box, "a") + " | " + SameTypeForms(null, null));
    Console.WriteLine(SameMember(new Shelf { Box = box }) + " | " + SameMember(new Shelf()));
    Console.WriteLine(ValueType(2) + " " + ValueType(1) + " " + ValueArm(2) + " " + ValueArm(1));
    Console.WriteLine(Interfaces(new Thing()) + " " + Interfaces(new Spot()) + " " + Interfaces(box));
    Console.WriteLine(InterfaceArm(new Thing()) + " " + InterfaceArm(new Spot()) + " " + InterfaceArm(box));
  }
}
