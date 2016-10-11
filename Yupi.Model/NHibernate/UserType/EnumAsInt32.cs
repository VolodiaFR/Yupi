﻿// ---------------------------------------------------------------------------------
// <copyright file="EnumAsInt32.cs" company="https://github.com/sant0ro/Yupi">
//   Copyright (c) 2016 Claudio Santoro, TheDoctor
// </copyright>
// <license>
//   Permission is hereby granted, free of charge, to any person obtaining a copy
//   of this software and associated documentation files (the "Software"), to deal
//   in the Software without restriction, including without limitation the rights
//   to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//   copies of the Software, and to permit persons to whom the Software is
//   furnished to do so, subject to the following conditions:
//
//   The above copyright notice and this permission notice shall be included in
//   all copies or substantial portions of the Software.
//
//   THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//   IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//   FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//   AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//   LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//   OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
//   THE SOFTWARE.
// </license>
// ---------------------------------------------------------------------------------
namespace Yupi.Model
{
    using System;
    using System.Data;
    using System.Diagnostics;

    using Headspring;

    using NHibernate;
    using NHibernate.SqlTypes;

    public class EnumAsInt32<T> : UserType
        where T : Headspring.Enumeration<T>
    {
        #region Properties

        public override Type ReturnedType
        {
            get { return typeof(T); }
        }

        public override SqlType[] SqlTypes
        {
            get { return new[] {SqlTypeFactory.Int32}; }
        }

        #endregion Properties

        #region Methods

        public override object NullSafeGet(System.Data.IDataReader rs, string[] names, object owner)
        {
            object obj = NHibernateUtil.Int32.NullSafeGet(rs, names);
            return Enumeration<T>.FromValue(Convert.ToInt32(obj));
        }

        public override void NullSafeSet(System.Data.IDbCommand cmd, object value, int index)
        {
            Debug.Assert(cmd != null);
            if (value == null)
            {
                ((IDataParameter) cmd.Parameters[index]).Value = DBNull.Value;
            }
            else
            {
                ((IDataParameter) cmd.Parameters[index]).Value = ((T) value).Value;
            }
        }

        #endregion Methods
    }
}