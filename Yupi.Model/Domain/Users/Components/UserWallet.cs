﻿#region Header

// ---------------------------------------------------------------------------------
// <copyright file="UserWallet.cs" company="https://github.com/sant0ro/Yupi">
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

#endregion Header

namespace Yupi.Model.Domain.Components
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class UserWallet
    {
        #region Properties

        [Required]
        public virtual int AchievementPoints
        {
            get; set;
        }

        [Ignore]
        public virtual IDictionary<ActivityPointsType, int> ActivityPoints
        {
            get; protected set;
        } = new Dictionary<ActivityPointsType, int> ();

        [Required]
        public virtual int Credits
        {
            get; set;
        }

        #endregion Properties

        #region Methods

        public virtual void AddActivityPoints(ActivityPointsType type, int value)
        {
            if (this.ActivityPoints.ContainsKey(type))
            {
                ActivityPoints.Add(type, value);
            }
            else
            {
                ActivityPoints[type] += value;
            }
        }

        public virtual int GetActivityPoints(ActivityPointsType type)
        {
            int value = 0;
            ActivityPoints.TryGetValue(type, out value);
            return value;
        }

        public virtual void SubstractActivityPoints(ActivityPointsType type, int value)
        {
            if (!this.ActivityPoints.ContainsKey(type))
            {
                throw new InvalidOperationException("User has no activity points of this type!");
            }

            ActivityPoints[type] -= value;
        }



        #endregion Methods
    }
}