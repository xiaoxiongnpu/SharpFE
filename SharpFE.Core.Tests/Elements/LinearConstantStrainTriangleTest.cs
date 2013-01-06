﻿//-----------------------------------------------------------------------
// <copyright file="?.cs" company="Iain Sproat">
//     Copyright Iain Sproat, 2012.
// </copyright>
//-----------------------------------------------------------------------
using System;
using MathNet.Numerics.LinearAlgebra.Double;
using NUnit.Framework;

namespace SharpFE.Core.Tests.Elements
{
	[TestFixture]
	public class LinearConstantStrainTriangleTest
	{
		private NodeFactory nodeFactory;
        private ElementFactory elementFactory;
        private FiniteElementNode node0;
        private FiniteElementNode node1;
        private FiniteElementNode node2;
		private GenericElasticMaterial material;
		private SolidRectangle section;
        private LinearConstantStrainTriangle SUT;
        
        [SetUp]
        public void SetUp()
        {
            nodeFactory = new NodeFactory(ModelType.Slab2D);
            node0 = nodeFactory.Create(1, 1);
            node1 = nodeFactory.Create(3, 1);
            node2 = nodeFactory.Create(2, 2);
			material = new GenericElasticMaterial(0, 0.1, 0, 0);
			section = new SolidRectangle(0.1, 1);
            elementFactory = new ElementFactory();
            SUT = elementFactory.CreateLinearConstantStrainTriangle(node0, node1, node2, material, section);
        }
        
		[Test]
		public void It_can_be_constructed()
		{
			Assert.IsNotNull(SUT);
			Assert.AreEqual(3, SUT.Nodes.Count);
			Assert.AreEqual(material, SUT.Material);
			Assert.AreEqual(section, SUT.CrossSection);
		}
		
		[Test]
		public void It_cannot_be_constructed_from_three_aligned_nodes()
		{
			Assert.Ignore();
		}
		
		[Test]
		public void It_will_warn_if_nodes_are_too_close()
		{
			Assert.Ignore();
		}
		
		[Test]
		public void It_can_calculate_X_axis()
		{
			Vector result = SUT.LocalXAxis;
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result[0]);
			Assert.AreEqual(0, result[1]);
			Assert.AreEqual(0, result[2]);
		}
		
		[Test]
		public void It_can_calculate_Y_axis()
		{
			Vector result = SUT.LocalYAxis;
			Assert.IsNotNull(result);
			Assert.AreEqual(0, result[0]);
			Assert.AreEqual(1, result[1]);
			Assert.AreEqual(0, result[2]);
		}
		
		[Test]
		public void It_can_calculate_Area()
		{
			Assert.AreEqual(1, SUT.Area);
		}
	}
}