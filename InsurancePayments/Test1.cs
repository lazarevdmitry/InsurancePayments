/*
 * Создано в SharpDevelop.
 * Пользователь: hobbit
 * Дата: 11.05.2016
 * Время: 15:26
 * 
 * Для изменения этого шаблона используйте меню "Инструменты | Параметры | Кодирование | Стандартные заголовки".
 */
using System;
using NUnit.Framework;

namespace InsurancePayments
{
	[TestFixture]
	public class Test1
	{
		[Test]
		public void TestMethod()
		{
			// TODO: Add your test.
			DateTime d0=DateTime.Now;
			DateTime d1=DateTime.Now;
			double s=0;
			string t="";
			double b=0;
			int days=0,months=0;
			double procent=0;
			double usch=0;
			Payment p0=new Payment(d0,s,t,d1,b);
			Assert.AreEqual(p0.getCountDays(),days,"Даты");
			Assert.AreEqual(p0.getCountMonths(),months);
			Assert.AreEqual(p0.getOutProcent(),procent);
			Assert.AreEqual(p0.getUscherb(),usch);
			
		}
	}
}
