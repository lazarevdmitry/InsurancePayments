/*
 * Создано в SharpDevelop.
 * Пользователь: hobbit
 * Дата: 11.05.2016
 * Время: 13:11
 * 
 * Для изменения этого шаблона используйте меню "Инструменты | Параметры | Кодирование | Стандартные заголовки".
 */
using System;
using Col=System.Collections;

namespace InsurancePayments
{
	/// <summary>
	/// Класс платежей
	/// </summary>
	public class Payment
	{
		private DateTime data0;
		private double inSumma;
		private String typeOfPayment;
		private int countDays;
		private double outProcent;
		private double countMonths;
		private double uscherb;
		private DateTime data1;
		private String stroki;
		private double bankstavka;
		
		private static String[] cbDates={"30.10.2009","25.11.2009","28.12.2009","24.02.2010","29.03.2010","30.04.2010","01.06.2010","29.02.2011","03.05.2011","26.12.2011","14.09.2012","01.01.2016"};
		private static double[] cbProcents={9.5,9,8.75,8.5,8.25,8,7.75,8,8.25,8,8.25,11};
		
		// Outputs
		public DateTime getData0(){
			return this.data0;
		}
		public double getInSumma(){
			return this.inSumma;
		}
		public String getTypeOfPayment(){
			return this.typeOfPayment;
		}
		public int getCountDays(){
			return this.countDays;
		}
		public double getOutProcent(){
			return this.outProcent;
		}
		public double getCountMonths(){
			return this.countMonths;
		}
		public double getUscherb(){
			return this.uscherb;
		}
		public DateTime getData1(){
			return this.data1;
		}
		public String getStroka(){
			return this.stroki;
		}
		// end of outputs
		
		private int calcDays(DateTime a, DateTime b)
		{
			return (1+360 * (b.Year - a.Year) + 30 * (b.Month - a.Month) + (b.Day - a.Day));
		}
		private double calcMonths(DateTime a, DateTime b)
		{			
			return ((double)Math.Round(((double)calcDays(a, b) / 30) * 10)) / 10;
		}
		private double Procent(double sum, double stavka, DateTime d0,DateTime d1){
			
			return ((double)Math.Round((sum / 360) * stavka * 0.01 * this.calcDays(d0,d1)* 100)) / 100;	// Округляем с точностью до 0.01 руб.
		}
		private double SumUscherb(double sum, double stavka, DateTime d0, DateTime d1){
			return Math.Round(sum*Math.Round(stavka/12*100)*0.0001*Math.Round(((double)this.calcMonths(d0,d1))*10/30)*10)/100;
		}
		
		public Payment(DateTime d0,double sum,string t,DateTime d1,double bankstavka)
		{
			this.data0=d0;
			this.data1=d1;
			this.countDays=calcDays(this.data0,this.data1);
			this.countMonths=calcMonths(this.data0,this.data1);
			this.inSumma=sum;
			this.typeOfPayment=t;
			this.bankstavka=bankstavka;
			// проверка даты изъятия средств
			// Если дата попадает в зону какой-либо ставки ЦБ, то
			// проводим рассчет 
			
			// ставки:
			// 01.01.16	11%
			// 14.09.12	8,25%
			// 26.12.11	8%
			// 03.05.11	8,25%
			// 28.02.11	8%
			// 01.06.10	7,75%
			// 30.04.10	8%
			// 29.03.10	8,25%
			// 24.02.10	8,5%
			// 28.12.09 8,75%
			// 25.11.09	9%
			// 30.10.09	9,5%
			DateTime cDate,cDateX;
			double cProcent;
			DateTime temp;
			for (int i=0;i<Payment.cbDates.Length;i++){
				//cDate=DateTime.Parse(Payment.cbDates[i]);
				Console.Write(DateTime.Now.ToString());
				cDate=DateTime.ParseExact(Payment.cbDates[i],"dd.mm.yyyy");
				
				//cDate=new DateTime();
				
				DateTime.TryParse(Payment.cbDates[i+1],out cDateX);
				
				cProcent=Payment.cbProcents[i]*0.01;
				temp=new DateTime();
				// Если date0 <= cDate, то
				if (DateTime.Compare(cDate,this.data0)>=0){
					temp=cDate;
				}
				// Если cDate<date0<cDate++, то
				if (DateTime.Compare(cDate,this.data0)<0&&DateTime.Compare(cDateX,this.data0)>0){
					temp=this.data0;
				}
				if (!temp.Equals(null)){
					double days=this.calcDays(temp,cDateX);
					double t1=Math.Round(this.inSumma*days*cProcent*100/360)*0.01;
					this.outProcent+=t1;
					string s=this.inSumma.ToString()+" руб. х "+days+" дней х "+cProcent.ToString()+"% / 360 = "+t1+" рублей;";
					this.stroki+=s;
				}
			}
			// Расчет суммы ущерба
			this.uscherb=SumUscherb(this.inSumma,this.bankstavka,this.data0,this.data1);
			
			
		}
	}
}
