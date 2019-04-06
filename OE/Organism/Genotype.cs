using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;


class Genotype
{
    BitArray bitArray;
    public Genotype(int length)
    {
        bitArray = new BitArray(length*32);
    }
    public uint this[int index]
    {
        get {
            var result = new uint[1];
            bitArray.CopyTo(result, index);
            return result[0];
        }
        set {

        }
    }

}
