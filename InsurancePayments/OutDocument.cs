/*
 * Создано в SharpDevelop.
 * Dmitry Lazarev
 * Дата: 30.04.2015
 * Время: 15:07
 * E-mail: lazarevdmitry2008@gmail.com
 * Для изменения этого шаблона используйте меню "Инструменты | Параметры | Кодирование | Стандартные заголовки".
 */
using System;
using System.Windows.Forms;
namespace InsurancePayments
{
	
	/// <summary>
	/// Класс для формирования RTF-файла
	/// 
	/// Основные этапы формирования файла:
	/// 1. Создание экземпляра класса
	/// 2. Ввод параметров документа
	/// 3. Проверка документа
	/// 4. Формирование документа
	/// 5. Вывод документа
	/// </summary>
	public class OutDocument
	{
		private string fileName;
		private string filePath;
		public enum toBank {BANK, STRAHOVAYA_KOMPANIYA};	
		private toBank sendToBank;// Определяет адресата данной претензии: 0 - Банк, 1 - Страховая компания
		public enum typeOfStrah {PERSONAL,COLLECTIVE};
		private typeOfStrah toStrah;
		private string bankName;	// Название банка
		private string skName;	//	Название страховой компании
		private string sProgramName;	//	Название страховой программы
		public enum Periodicity {EVERY_MONTH, SINGLE};	
		private Periodicity period;//	Периодичность вычетов страховой премии
		private double sumOfPayment;	//	Сумма выплат
		private DateTime dateOfPretention;	//	Дата подачи претензии
		
		public OutDocument()
		{
			
		}
		// Секция ввода параметров
		public void setFileName(string x){fileName=x;}
		public void setFilePath(string x){filePath=x;}
		public void setToBank(toBank x){sendToBank=x;}
		public void setTypeOfStrah(typeOfStrah x){toStrah=x;}
		public void setBankName(string x){bankName=x;}
		public void setSKNAme(string x){skName=x;}
		public void setSProgramName(string x){sProgramName=x;}
		public void setPeriod(Periodicity x){period=x;}
		public void setSumOfPayment(double x){sumOfPayment=x;}
		public void setDateOfPretention(DateTime x){dateOfPretention=x;}
		
		// Конец секции ввода параметров
		// Секция проверки документа
		public bool chDoc(){
			
			return true;
		}
		// Конец секции проверки документа
		// Секция формирования документа
		public bool formDoc(){
			
			return true;
		}
		// Конец секции формирования документа
		// Секция вывода документа
		public bool outDoc(){
		
			return true;
		}
		// Конец секции вывода документа
	}
}
