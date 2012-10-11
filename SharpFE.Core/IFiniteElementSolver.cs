﻿//-----------------------------------------------------------------------
// <copyright file="IFiniteElementSolver.cs" company="SharpFE">
//     Copyright Iain Sproat, 2012.
// </copyright>
//-----------------------------------------------------------------------

namespace SharpFE
{
    using System;
    
    /// <summary>
    /// Solvers of finite element models should inherit from this interface.
    /// </summary>
    /// <remarks>
    /// This interface is analagous the command interface of the Command pattern.
    /// </remarks>
    public interface IFiniteElementSolver
    {
        /// <summary>
        /// When the model is to be solved for the unknown results, this method should be called.
        /// </summary>
        /// <returns>A set of results of generated by this solver</returns>
        FiniteElementResults Solve();
    }
}
