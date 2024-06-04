using System;
using System.Collections;
using System.Collections.Generic;

namespace hashes
{
    public class ReadonlyBytes : IEnumerable
    {
        private int hashCode;
        private readonly byte[] data;
        public ReadonlyBytes(params byte[] byteArr)
        {
            if (byteArr is null) throw new ArgumentNullException();
            data = byteArr;
        }

        public byte this[int index] { get { return data[index]; } }

        public int Length { get { return data.Length; } }

        public override bool Equals(object obj)
        {
            return obj != null && obj.GetType() == GetType() && obj.GetHashCode() == GetHashCode();
        }

        public override string ToString()
        {
            return $"[{string.Join(", ", data)}]";
        }
        public override int GetHashCode()
        {
            unchecked
            {
                if (hashCode == 0)
                {
                    var temp = 1;
                    foreach (var item in data)

                    {
                        temp *= 2967427;
                        temp -= item;
                    }
                    hashCode = temp;

                }
                return hashCode;
            }
        }
        public IEnumerator<byte> GetEnumerator()
        {
            foreach (var item in data)
                yield return item;
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}