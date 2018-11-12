/*
 * Создано в SharpDevelop.
 * Пользователь: hobbit
 * Дата: 12.02.2016
 * Время: 11:37
 * 
 * Для изменения этого шаблона используйте меню "Инструменты | Параметры | Кодирование | Стандартные заголовки".
 */
using System;
using C=System.Collections;

namespace InsurancePayments.dev
{
	/// <summary>
	/// Класс для представления модели расчета страховых выплат в одном агрегированном классе.
	/// 
	/// </summary>
	public class InsurancePaymentsFactory
	{
		private double ItogovayaSummaKVozmescheniyu=0;
		private double SummaUscherbaZaPeriod=0;
		private double ObschayaSummaProzentovZaPeriod=0;
		private double SummaIzyatyhZaVesPeriodSredstv=0;
		
//		private double Zapisi=0;
		private FIO FIO=null;
		private string ContractNo=null;
		private double StavkaZBRF=0;
		private double StavkaBanka=0;
		private string BankName=null;
		private string PassportData=null;
		private DateTime ContractDate;
		private DateTime PretentionDate;
		private DateTime RasshetDate;
		private string RegistrationAddress=null;
		private string AgentContractNo=null;
		
		private C.ArrayList table=new C.ArrayList();
		
		public double Neustoika=0;
		public double Moral=0;
		public double ItogoBezShtrafa=0;
		
		/* Методы для ввода полей*/
		
		/*
		public void setItogovayaSummaKVozmescheniyu(double x){}
		public void setSummaUscherbaZaPeriod(double x){}
		public void setObschayaSummaProzentovZaPeriod(double x){}
		public void setSummaIzyatyhZaVesPeriodSredstv(){}
		
		public void setZapisi(){}
		*/
		public void setFIO(FIO x){
			this.FIO=x;
		}
		public void setContractNo(string x){
			this.ContractNo=x;
		}
		public void setStavkaZBRF(double x){
			this.StavkaZBRF=x;
		}
		public void setStavkaBanka(double x){
			this.StavkaBanka=x;
		}
		public void setBankName(string x){
			this.BankName=x;
		}
		public void setPassportData(string x){
			this.PassportData=x;
		}
		public void setContractDate(DateTime x){
			this.ContractDate=x;
		}
		public void setPretentionDate(DateTime x){
			this.PretentionDate=x;
		}
		public void setRasshetDate(DateTime x){
			this.RasshetDate=x;
		}
		public void setRegistrationAddress(string x){
			this.RegistrationAddress=x;
		}
		public void setAgentContractNo(string x){
			this.AgentContractNo=x;
		}
		
		/* Методы для вывода полей*/
		
		public double getItogovayaSummaKVozmescheniyu(){
			return this.ItogovayaSummaKVozmescheniyu;
		}
		public double getSummaUscherbaZaPeriod(){
			return this.SummaUscherbaZaPeriod;
		}
		public double getObschayaSummaProzentovZaPeriod(){
			return this.ObschayaSummaProzentovZaPeriod;
		}
		public double getSummaIzyatyhZaVesPeriodSredstv(){
			return this.SummaIzyatyhZaVesPeriodSredstv;
		}
		/* public void getZapisi(){} */
		public FIO getFIO(){
			return this.FIO;
		}
		public string getContractNo(){
			return this.ContractNo;
		}
		public double getStavkaZBRF(){
			return this.StavkaZBRF;
		}
		public double getStavkaBanka(){
			return this.StavkaBanka;
		}
		public string getBankName(){
			return this.BankName;
		}
		public string getPassportData(){
			return this.PassportData;
		}
		public DateTime getContractDate(){
			return this.ContractDate;
		}
		public DateTime getPretentionDate(){
			return this.PretentionDate;
		}
		public DateTime getRasshetDate(){
			return this.RasshetDate;
		}
		public string getRegistrationAddress(){
			return this.RegistrationAddress;
		}
		public string getAgentContractNo(){
			return this.AgentContractNo;
		}
			
		/* Методы для ввода, редактирования и удаления записей*/
		public void addTableItem(TableItem t){
			table.Add(t);
			
		}
		public void editTableItem(int index,TableItem t){
			table.Insert(index,t);
		}
		public TableItem getTableItem(int index){
			TableItem[] ta=(TableItem[])table.ToArray();
			return ta[index];
		}
		public void deleteTableItem(int index){
			table.RemoveAt(index);
		}
		/* Очистка таблицы*/
		public void clearTable(){
			table.Clear();
		}
		
		public void calc(){
			if (required()){
				C.IEnumerator en=table.GetEnumerator();
				TableItem y=(TableItem)en.Current;
				// Рассчет количества дней
				countDays(y,this.RasshetDate);
				// Расчет процентов за пользование денежными средствами
				procentyZaPolzDenSredstvami(y);
				// Количество месяцев
				countMonths(y);
				// Сумма ущерба
				summaUscherba(y);
				// Сумма изъятых средств
				summaIzyatyhSredstv(en);
				// Общая сумма процентов за весь период
				summaProzentovZaPeriod(en);
				// Сумма ущерба за период
				summaUscherbaZaPeriod(en);
				// Неустойка за неудовлетворение претензии
				neustoika();
				// Моральный ущерб
				moralUscherb();
				// Итого без учета штрафа
				itogoBezShtrafa();
				// Штраф 50% от суммы
				shtraf50();
				// Итоговая сумма к возмещению
				itogo();
			} else {
				
			}
		
		}
		private void itogo(){
		
		}
		private void shtraf50(){
			double t=Math.Round(50*this.ItogoBezShtrafa)*0.01;
			this.ItogoBezShtrafa=t+this.ItogoBezShtrafa;
		}
		private void itogoBezShtrafa(){
			double t=this.SummaUscherbaZaPeriod+this.SummaIzyatyhZaVesPeriodSredstv+this.ObschayaSummaProzentovZaPeriod+this.Neustoika+this.Moral;
			this.ItogoBezShtrafa=t;
		}
		private void moralUscherb(){
			this.Moral=5000;
		}
		private void neustoika(){
			DateTime a=this.PretentionDate;
			DateTime b=this.RasshetDate;
			a.AddDays(10);
			int d=360*(b.Year-a.Year)+12*(b.Month-a.Month)+(b.Day-a.Day)-1;
			this.Neustoika=Math.Round(this.SummaIzyatyhZaVesPeriodSredstv*3*d)*0.01;
		}
		private void summaUscherbaZaPeriod(C.IEnumerator e){
			double t=((TableItem)e.Current).getSum();
			while (e.MoveNext()){
				t+=((TableItem)e.Current).getSum();
			}
			this.SummaUscherbaZaPeriod=t;
		}
		private void summaProzentovZaPeriod(C.IEnumerator e){
			double t=((TableItem)e.Current).getProcent();
			while (e.MoveNext()){
				t+=((TableItem)e.Current).getProcent();
			}
			this.ObschayaSummaProzentovZaPeriod=t;
		}
		private void summaIzyatyhSredstv(C.IEnumerator e){
			double t=((TableItem)e.Current).getVal();
			while (e.MoveNext()){
				t+=((TableItem)e.Current).getVal();
			}
			this.SummaIzyatyhZaVesPeriodSredstv=t;
		}
		private void summaUscherba(TableItem temp){
			// !!!!
			double t=Math.Round(temp.getVal()*temp.getCountMonths()*this.StavkaBanka*100/12)*0.01;
			temp.setSum(t);
			// !!!!
		}
		private void countMonths(TableItem temp){
			// !!!!
			temp.setCountMonths((double)Math.Round((double)temp.getCountDays()*100/30)*0.01);
			// !!!!
		}
		private void procentyZaPolzDenSredstvami(TableItem temp){
			// !!!!
			double t=(temp.getVal()*temp.getCountDays()*this.StavkaZBRF)/360;
			t=Math.Round(t*100)*0.01;
			temp.setProcent(t);
			// !!!!
		}
		private void countDays(TableItem temp,DateTime b){
			DateTime a=temp.getDate();
			int d=360*(b.Year-a.Year)+12*(b.Month-a.Month)+(b.Day-a.Day)-1;
			temp.setCountDays(d);
		}
		private bool required(){
			bool temp=this.StavkaZBRF.Equals(null)&&this.StavkaBanka.Equals(null)&&this.ContractDate.Equals(null)&&this.PretentionDate.Equals(null)&&this.RasshetDate.Equals(null)&&table.Count!=0;
			return temp;
		}
		
		public InsurancePaymentsFactory(double stavkaZBRF, double stavkaBanka, DateTime contractDate, DateTime pretentionDate, DateTime rasshetDate, TableItem[] tableItems)
		{
			this.StavkaZBRF=stavkaZBRF;
			this.StavkaBanka=stavkaBanka;
			this.ContractDate=contractDate;
			this.PretentionDate=pretentionDate;
			this.RasshetDate=rasshetDate;
			int i;
			for (i=0;i<tableItems.Length;i++){
				table.Add(tableItems[i]);
			}
		}
	}
}
