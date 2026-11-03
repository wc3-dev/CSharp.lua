using System;

/// @CSharpLua.Ignore
public class handle {
}

/// @CSharpLua.Ignore
public class widget : handle {
}

/// @CSharpLua.Ignore
public class unit : widget {
}

public static class Natives {
  /// @CSharpLua.Template = "io.stdout"
  public static extern widget AnyWidget();

  /// @CSharpLua.Template = "type({0})"
  public static extern string TypeOf(object value);
}

class Program {
  public static void Main() {
    widget w = Natives.AnyWidget();
    var u = (unit)w;
    Console.WriteLine(Natives.TypeOf(u));
    object o = w;
    var v = (unit)o;
    Console.WriteLine(Natives.TypeOf(v));
    object[] items = { w };
    foreach (unit item in items) {
      Console.WriteLine(Natives.TypeOf(item));
    }
    var none = (unit)(object)null;
    Console.WriteLine(none == null);
  }
}
