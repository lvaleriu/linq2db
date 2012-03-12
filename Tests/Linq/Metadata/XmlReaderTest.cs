﻿using System;
using System.Data.Linq.Mapping;
using System.IO;
using System.Text;

using LinqToDB_Temp.Metadata;

using NUnit.Framework;

namespace Tests.Metadata
{
	public class XmlReaderTest
	{
		const string Data =
			@"<?xml version='1.0' encoding='utf-8' ?>
			<Types xmlns='urn:schemas-bltoolkit-net:typeext'>
				<Type Name='MyType'>
					<Member Name='Field1'>
						<!-- 12345 -->
						<Attr1>
							<Value1 Value='2' Type='System.Int32' />
						</Attr1>
						<Attr2>
							<Value1 Value='3' />
						</Attr2>
					</Member>
					<Attr3><Value1 Value='4' Type='System.Int32' /></Attr3>
				</Type>

				<Type Name='XmlReaderTest'>
					<Table>
						<Name Value='TestName' />
					</Table>
					<Member Name='Field1'>
						<ColumnAttribute>
							<Name Value='TestName' />
						</ColumnAttribute>
					</Member>
					<Member Name='Property1'>
						<System.Data.Linq.Mapping.ColumnAttribute>
							<Name Value='TestName' />
						</System.Data.Linq.Mapping.ColumnAttribute>
					</Member>
				</Type>
			</Types>";

		[Test]
		public void Parse()
		{
			new XmlAttributeReader(new MemoryStream(Encoding.UTF8.GetBytes(Data)));
		}

		[Test]
		public void TypeAttribute()
		{
			var rd    = new XmlAttributeReader(new MemoryStream(Encoding.UTF8.GetBytes(Data)));
			var attrs = rd.GetAttributes<TableAttribute>(typeof(XmlReaderTest));

			Assert.NotNull (attrs);
			Assert.AreEqual(1, attrs.Length);
			Assert.AreEqual("TestName", attrs.Head.Name);
		}

		public int Field1;

		[Test]
		public void FieldAttribute()
		{
			var rd    = new XmlAttributeReader(new MemoryStream(Encoding.UTF8.GetBytes(Data)));
			var attrs = rd.GetAttributes<ColumnAttribute>(typeof(XmlReaderTest), "Field1");

			Assert.NotNull (attrs);
			Assert.AreEqual(1, attrs.Length);
			Assert.AreEqual("TestName", attrs.Head.Name);
		}

		public int Property1 { get; set; }

		[Test]
		public void PropertyAttribute()
		{
			var rd    = new XmlAttributeReader(new MemoryStream(Encoding.UTF8.GetBytes(Data)));
			var attrs = rd.GetAttributes<ColumnAttribute>(typeof(XmlReaderTest), "Property1");

			Assert.NotNull (attrs);
			Assert.AreEqual(1, attrs.Length);
			Assert.AreEqual("TestName", attrs.Head.Name);
		}
	}
}