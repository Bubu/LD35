using System;

public class Tuple<T1, T2>
{
	public T1 First { get; private set; }
	public T2 Second { get; private set; }
	internal Tuple(T1 first, T2 second)
	{
		First = first;
		Second = second;
	}

	public override bool Equals(System.Object obj)
	{
		// If parameter is null return false.
		if (obj == null)
		{
			return false;
		}

		// If parameter cannot be cast to Point return false.
		Tuple<T1,T2> p = (Tuple<T1,T2>)obj;
		if ((System.Object)p == null)
		{
			return false;
		}

		// Return true if the fields match:
		return (this.First.Equals(p.First)) && (this.Second.Equals(p.Second));
	}

	public bool Equals(Tuple<T1,T2> p)
	{
		// If parameter is null return false:
		if ((object)p == null)
		{
			return false;
		}

		// Return true if the fields match:
		return (First.Equals(p.First)) && (Second.Equals(p.Second));
	}

	public override int GetHashCode()
	{
		return First.GetHashCode() ^ Second.GetHashCode();
	}
}

public static class Tuple
{
	public static Tuple<T1, T2> New<T1, T2>(T1 first, T2 second)
	{
		var tuple = new Tuple<T1, T2>(first, second);
		return tuple;
	}
}