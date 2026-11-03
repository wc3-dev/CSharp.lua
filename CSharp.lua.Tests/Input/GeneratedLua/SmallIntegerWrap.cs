using System;

class Holder {
  public byte Field;
  public short Prop { get; set; }
}

class Program {
  public static void Main() {
    byte b = 250;
    b += 10;
    Console.WriteLine(b);
    b -= 5;
    Console.WriteLine(b);
    b++;
    Console.WriteLine(b);
    b--;
    Console.WriteLine(b);
    b *= 2;
    Console.WriteLine(b);
    b <<= 1;
    Console.WriteLine(b);
    sbyte s = 120;
    s += 10;
    Console.WriteLine(s);
    short h = 32767;
    h++;
    Console.WriteLine(h);
    --h;
    Console.WriteLine(h);
    short m = -32768;
    m /= -1;
    Console.WriteLine(m);
    ushort u = 0;
    u--;
    Console.WriteLine(u);
    var bytes = new byte[] { 255 };
    bytes[0]++;
    bytes[0] += 3;
    Console.WriteLine(bytes[0]);
    var holder = new Holder { Field = 255, Prop = short.MinValue };
    holder.Field += 2;
    holder.Prop--;
    Console.WriteLine(holder.Field + " " + holder.Prop);
    byte c = 255;
    int was = c++;
    Console.WriteLine(was + " " + c);
    byte d = 255;
    int now = ++d;
    Console.WriteLine(now + " " + d);
    int i = 255;
    i++;
    Console.WriteLine(i);
  }
}
