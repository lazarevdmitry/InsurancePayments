/*
 * Создано в SharpDevelop.
 * Dmitry Lazarev
 * Дата: 30.04.2015
 * Время: 12:39
 * E-mail: lazarevdmitry2008@gmail.com
 * Для изменения этого шаблона используйте меню "Инструменты | Параметры | Кодирование | Стандартные заголовки".
 */
using System;
using System.Windows.Forms;

namespace InsurancePayments
{
	/// <summary>
	/// Класс, служащий хранилищем информации о банках
	/// </summary>
	public class Banks
	{
		//private System.Collections.SortedList banks;
		System.Collections.Generic.Dictionary<String, String> banks;
		private String[] bankList;
		private String[] addressList;
		
		public Banks()
		{
			// Инициализация списка
			banks = new System.Collections.Generic.Dictionary<String, String>();
			try {
				// Открытие файла с банками
				bankList=System.IO.File.ReadAllLines("banks.txt");
				
				// Открытие файла с адресами банков
				addressList=System.IO.File.ReadAllLines("addresses.txt");
			} catch (System.IO.IOException e){
				MessageBox.Show(e.Message);
				return;
			}
			if (bankList.Length!=addressList.Length){
			
				return;
			}
			for (int i=0;i<bankList.Length-1;i++){
				banks.Add(bankList[i],addressList[i]);
			}
			MessageBox.Show("Список банков загружен");
		}
		public string getAddress(String b){
			//String rez;
			try {
				System.Collections.IDictionaryEnumerator ie=banks.GetEnumerator();
				
				while (((String)ie.Key)!=b){
					ie.MoveNext();
				};
				return (string)ie.Value;
			} catch (IndexOutOfRangeException e){
				MessageBox.Show(e.Message);
				return null;
			}
		}
		public string[] getKeys(){
			
			string[] outs=new string[banks.Count];
			banks.Keys.CopyTo(outs,0);
			
			return outs;
		}
		
		
		
		
	}
}
