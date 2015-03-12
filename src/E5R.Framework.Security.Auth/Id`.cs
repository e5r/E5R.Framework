// Copyright (C) E5R Development Team. All rights reserved.
// Licensed under the MIT License. See LICENSE file for license information.

using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Security.Cryptography;

namespace E5R.Framework.Security.Auth
{
    public interface _GSize { int _ { get; } }
    public class IdSize32 : _GSize { int _GSize._ { get { return 32; } } }
    public class IdSize40 : _GSize { int _GSize._ { get { return 40; } } }
    public class IdSize64 : _GSize { int _GSize._ { get { return 64; } } }
    public class IdSize96 : _GSize { int _GSize._ { get { return 96; } } }
    public class IdSize128 : _GSize { int _GSize._ { get { return 128; } } }

    public interface _GAlgorithm { HashAlgorithm _ { get; } }
    public class AlgorithmMD5 : _GAlgorithm { HashAlgorithm _GAlgorithm._ { get { return MD5.Create(); } } }
    public class AlgorithmSHA1 : _GAlgorithm { HashAlgorithm _GAlgorithm._ { get { return SHA1.Create(); } } }
    public class AlgorithmSHA256 : _GAlgorithm { HashAlgorithm _GAlgorithm._ { get { return SHA256.Create(); } } }
    public class AlgorithmSHA384 : _GAlgorithm { HashAlgorithm _GAlgorithm._ { get { return SHA384.Create(); } } }
    public class AlgorithmSHA512 : _GAlgorithm { HashAlgorithm _GAlgorithm._ { get { return SHA512.Create(); } } }

    public class Id<T, E, S> : IDisposable
        where T : _GAlgorithm
                , new()
        where E : Encoding
                , new()
        where S : _GSize
                , new()
    {
        private readonly HashAlgorithm _algorithm = new T()._;
        private readonly E _encoding = new E();
        private readonly int _size = new S()._;
        private readonly string _value = null;

        public Id()
        {
            _value = Guid.NewGuid().ToString().Replace("-", string.Empty);

            var hash = _algorithm.ComputeHash(_encoding.GetBytes(_value));

            _value = string.Empty;

            foreach (var h in hash)
            {
                _value += h.ToString("x2");
            }

            Validate();
        }

        public Id(string value)
        {
            _value = value;

            Validate();
        }

        private void Validate()
        {
            if (_value != null && _value.Length == _size)
            {
                if (new Regex(@"^[0-9a-fA-F]+$").IsMatch(_value))
                {
                    return;
                }
            }
            throw new FormatException(string.Format("Id({0}) invalid format.", _value));
        }

        public override string ToString()
        {
            return _value;
        }

        public override int GetHashCode()
        {
            return _value.GetHashCode();
        }

        public byte[] ToBytes()
        {
            return ToBytes(_encoding);
        }

        public byte[] ToBytes(Encoding encoding)
        {
            return encoding.GetBytes(_value);
        }

        public void Dispose()
        {
            _algorithm.Dispose();
        }
    }
}
