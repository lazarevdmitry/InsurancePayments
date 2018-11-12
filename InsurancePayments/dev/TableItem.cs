/*
 * Создано в SharpDevelop.
 * Пользователь: Dmitry Lazarev
 * Дата: 12.02.2016
 * Время: 12:04
 * 
 * Для изменения этого шаблона используйте меню "Инструменты | Параметры | Кодирование | Стандартные заголовки".
 */
using System;

namespace InsurancePayments.dev
{
	/// <summary>
	/// Класс модели записи (строки) таблицы
	/// </summary>
	public class TableItem
	{
		private DateTime date;
		private double val;
		private string type;
		
		private int countDays;
		private double procent;
		
		private double countMonths;
		private double sum;
		
		public void setDate(DateTime x){
			this.date=x;
		}
		public DateTime getDate(){
			return this.date;
		}
		public void setVal(double x){
			this.val=x;
		}
		public double getVal(){
			return this.val;
		}
		public void setType(string x){
			this.type=x;
		}
		public string getType(){
			return this.type;
		}
		public void setCountDays(int x){
			this.countDays=x;
		}
		
		public int getCountDays(){
			return this.countDays;
		}
		
		public void setProcent(double x){
			this.procent=x;
		}
		
		public double getProcent(){
			return this.procent;
		}
		
		public void setCountMonths(double x){
			this.countMonths=x;
		}
		
		public double getCountMonths(){
			return this.countMonths;
		}
		
		public void setSum(double x){
			this.sum=x;
		}
		
		public double getSum(){
			return this.sum;
		}
		public TableItem()
		{
			
		}
	}
}
