﻿using Lingualizr.Bytes;

namespace Lingualizr.Tests.Bytes;

public class ArithmeticTests
{
    [Fact]
    public void Add()
    {
        ByteSize size1 = ByteSize.FromBytes(1);
        ByteSize result = size1.Add(size1);

        Assert.Equal(2, result.Bytes);
        Assert.Equal(16, result.Bits);
    }

    [Fact]
    public void AddBits()
    {
        ByteSize size = ByteSize.FromBytes(1).AddBits(8);

        Assert.Equal(2, size.Bytes);
        Assert.Equal(16, size.Bits);
    }

    [Fact]
    public void AddBytes()
    {
        ByteSize size = ByteSize.FromBytes(1).AddBytes(1);

        Assert.Equal(2, size.Bytes);
        Assert.Equal(16, size.Bits);
    }

    [Fact]
    public void AddKilobytes()
    {
        ByteSize size = ByteSize.FromKilobytes(2).AddKilobytes(2);

        Assert.Equal(4 * 1024 * 8, size.Bits);
        Assert.Equal(4 * 1024, size.Bytes);
        Assert.Equal(4, size.Kilobytes);
    }

    [Fact]
    public void AddMegabytes()
    {
        ByteSize size = ByteSize.FromMegabytes(2).AddMegabytes(2);

        Assert.Equal(4 * 1024 * 1024 * 8, size.Bits);
        Assert.Equal(4 * 1024 * 1024, size.Bytes);
        Assert.Equal(4 * 1024, size.Kilobytes);
        Assert.Equal(4, size.Megabytes);
    }

    [Fact]
    public void AddGigabytes()
    {
        ByteSize size = ByteSize.FromGigabytes(2).AddGigabytes(2);

        Assert.Equal(4d * 1024 * 1024 * 1024 * 8, size.Bits);
        Assert.Equal(4d * 1024 * 1024 * 1024, size.Bytes);
        Assert.Equal(4d * 1024 * 1024, size.Kilobytes);
        Assert.Equal(4d * 1024, size.Megabytes);
        Assert.Equal(4d, size.Gigabytes);
    }

    [Fact]
    public void AddTerabytes()
    {
        ByteSize size = ByteSize.FromTerabytes(2).AddTerabytes(2);

        Assert.Equal(4d * 1024 * 1024 * 1024 * 1024 * 8, size.Bits);
        Assert.Equal(4d * 1024 * 1024 * 1024 * 1024, size.Bytes);
        Assert.Equal(4d * 1024 * 1024 * 1024, size.Kilobytes);
        Assert.Equal(4d * 1024 * 1024, size.Megabytes);
        Assert.Equal(4d * 1024, size.Gigabytes);
        Assert.Equal(4d, size.Terabytes);
    }

    [Fact]
    public void Subtract()
    {
        ByteSize size = ByteSize.FromBytes(4).Subtract(ByteSize.FromBytes(2));

        Assert.Equal(16, size.Bits);
        Assert.Equal(2, size.Bytes);
    }

    [Fact]
    public void IncrementOperator()
    {
        ByteSize size = ByteSize.FromBytes(2);
        size++;

        Assert.Equal(24, size.Bits);
        Assert.Equal(3, size.Bytes);
    }

    [Fact]
    public void NegativeOperator()
    {
        ByteSize size = ByteSize.FromBytes(2);

        size = -size;

        Assert.Equal(-16, size.Bits);
        Assert.Equal(-2, size.Bytes);
    }

    [Fact]
    public void DecrementOperator()
    {
        ByteSize size = ByteSize.FromBytes(2);
        size--;

        Assert.Equal(8, size.Bits);
        Assert.Equal(1, size.Bytes);
    }

    [Fact]
    public void PlusOperator()
    {
        ByteSize size1 = ByteSize.FromBytes(1);
        ByteSize size2 = ByteSize.FromBytes(1);

        ByteSize result = size1 + size2;

        Assert.Equal(2, result.Bytes);
    }

    [Fact]
    public void MinusOperator()
    {
        ByteSize size1 = ByteSize.FromBytes(2);
        ByteSize size2 = ByteSize.FromBytes(1);

        ByteSize result = size1 - size2;

        Assert.Equal(1, result.Bytes);
    }
}
