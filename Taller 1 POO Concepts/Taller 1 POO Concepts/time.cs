using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Taller_1_POO_Concepts;
    public class Time
{
    private int _hour;
    private int _minute;
    private int _second;
    private int _millisecond;

    public int Hour
    {
        get => _hour;
        set { if (ValidHour(value)) _hour = value; else throw new ArgumentException("Hora inválida"); }
    }
    public int Minute
    {
        get => _minute;
        set { if (ValidMinute(value)) _minute = value; else throw new ArgumentException("Minuto inválido"); }
    }
    public int Second
    {
        get => _second;
        set { if (ValidSecond(value)) _second = value; else throw new ArgumentException("Segundo inválido"); }
    }
    public int Millisecond
    {
        get => _millisecond;
        set { if (ValidMillisecond(value)) _millisecond = value; else throw new ArgumentException("Milisegundo inválido"); }
    }

    public Time() : this(0, 0, 0, 0) { }
    public Time(int hour) : this(hour, 0, 0, 0) { }
    public Time(int hour, int minute) : this(hour, minute, 0, 0) { }
    public Time(int hour, int minute, int second) : this(hour, minute, second, 0) { }
    public Time(int hour, int minute, int second, int millisecond)
    {
        Hour = hour;
        Minute = minute;
        Second = second;
        Millisecond = millisecond;
    }

    public override string ToString()
    {
        try
        {
            DateTime time = new DateTime(1, 1, 1, _hour, _minute, _second, _millisecond);
            return time.ToString("hh:mm:ss.fff tt");
        }
        catch (Exception)
        {
            throw new ArgumentException("Hora inválida");
        }
    }

    public int ToMilliseconds() => ValidTime() ? (_hour * 3600000) + (_minute * 60000) + (_second * 1000) + _millisecond : 0;
    public int ToSeconds() => ValidTime() ? (_hour * 3600) + (_minute * 60) + _second : 0;
    public int ToMinutes() => ValidTime() ? (_hour * 60) + _minute : 0;

    public bool IsOtherDay(Time other)
    {
        return (this.ToMilliseconds() + other.ToMilliseconds()) >= 86400000;
    }

    public Time Add(Time other)
    {
        int totalMillis = this.ToMilliseconds() + other.ToMilliseconds();
        int newHour = (totalMillis / 3600000) % 24;
        int newMinute = (totalMillis / 60000) % 60;
        int newSecond = (totalMillis / 1000) % 60;
        int newMillisecond = totalMillis % 1000;
        return new Time(newHour, newMinute, newSecond, newMillisecond);
    }

    private bool ValidTime() => ValidHour(_hour) && ValidMinute(_minute) && ValidSecond(_second) && ValidMillisecond(_millisecond);
    public static bool ValidHour(int hour) => hour >= 0 && hour < 24;
    public static bool ValidMinute(int minute) => minute >= 0 && minute < 60;
    public static bool ValidSecond(int second) => second >= 0 && second < 60;
    public static bool ValidMillisecond(int millisecond) => millisecond >= 0 && millisecond < 1000;
}

