﻿//-----------------------------------------------------------------------
// <copyright file="?.cs" company="Iain Sproat">
//     Copyright Iain Sproat, 2012.
// </copyright>
//-----------------------------------------------------------------------
using System;
using MathNet.Numerics.LinearAlgebra.Double;

namespace SharpFE
{
	/// <summary>
	/// Triangular shaped element which calculates membrane forces only
	/// </summary>
	public class LinearConstantStrainTriangle : FiniteElement, IHasMaterial, IHasCrossSection
	{
		public LinearConstantStrainTriangle(FiniteElementNode node0, FiniteElementNode node1, FiniteElementNode node2, IMaterial mat, ICrossSection section)
		{
			this.AddNode(node0);
			this.AddNode(node1);
			this.AddNode(node2);
			
			Guard.AgainstNullArgument(mat, "mat");
			Guard.AgainstNullArgument(section, "section");
			this.Material = mat;
			this.CrossSection = section;
		}
		
		public IMaterial Material
		{
			get;
			private set;
		}
		
		public ICrossSection CrossSection
		{
			get;
			private set;
		}
		
		
		/// <summary>
		/// Gets or sets the vector representing the local x axis
		/// </summary>
		public override Vector LocalXAxis
		{
			get
			{
				double initialLengthOfSide1ProjectedInXAxis = this.Nodes[1].OriginalX - this.Nodes[0].OriginalX;
				double initialLengthOfSide1ProjectedInYAxis = this.Nodes[1].OriginalY - this.Nodes[0].OriginalY;
				double initialLengthOfSide1ProjectedInZAxis = this.Nodes[1].OriginalZ - this.Nodes[0].OriginalZ;
				DenseVector result = new DenseVector(new double[]
				                                     {
				                                     	initialLengthOfSide1ProjectedInXAxis,
				                                     	initialLengthOfSide1ProjectedInYAxis,
				                                     	initialLengthOfSide1ProjectedInZAxis
				                                     });
				return (Vector)result;
			}
		}
		
		/// <summary>
		/// Gets the vector representing the direction of the local y axis
		/// </summary>
		/// <remarks>
		/// Uses the right-angled vector from side1 to the third point as the Y-axis.
		/// </remarks>
		public override Vector LocalYAxis
		{
			get
			{
				Vector result = Geometry.VectorBetweenPointAndLine(this.Nodes[2].AsVector(), this.Nodes[0].AsVector(), this.LocalXAxis);
				result = (Vector)result.Negate();
				return result;
			}
		}
		
		public double Area
		{
			get
			{
				return Geometry.AreaTriangle(this.Nodes[0], this.Nodes[1], this.Nodes[2]);
			}
		}
		
		private Vector Side3
		{
			get
			{
				double initialLengthOfSide1ProjectedInXAxis = this.Nodes[0].OriginalX - this.Nodes[2].OriginalX;
				double initialLengthOfSide2ProjectedInYAxis = this.Nodes[0].OriginalY - this.Nodes[2].OriginalY;
				double initialLengthOfSide3ProjectedInZAxis = this.Nodes[0].OriginalZ - this.Nodes[2].OriginalZ;
				DenseVector result = new DenseVector(new double[]
				                                     {
				                                     	initialLengthOfSide1ProjectedInXAxis,
				                                     	initialLengthOfSide2ProjectedInYAxis,
				                                     	initialLengthOfSide3ProjectedInZAxis
				                                     });
				return (Vector)result;
			}
		}
		
		public override bool IsASupportedBoundaryConditionDegreeOfFreedom(DegreeOfFreedom degreeOfFreedom)
		{
			switch(degreeOfFreedom)
			{
				case DegreeOfFreedom.X:
				case DegreeOfFreedom.Y:
					return true;
				case DegreeOfFreedom.Z:
				case DegreeOfFreedom.XX:
				case DegreeOfFreedom.YY:
				case DegreeOfFreedom.ZZ:
				default:
					return false;
			}
		}
		
		
		
		protected override void ThrowIfNodeCannotBeAdded(FiniteElementNode nodeToAdd)
		{
			if (this.Nodes.Count > 2)
			{
				throw new ArgumentException("Cannot add more than 3 nodes");
			}
		}
	}
}