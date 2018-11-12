/*
 * Создано в SharpDevelop.
 * Dmitry Lazarev
 * Дата: 30.04.2015
 * Время: 14:41
 * E-mail: lazarevdmitry2008@gmail.com
 * Для изменения этого шаблона используйте меню "Инструменты | Параметры | Кодирование | Стандартные заголовки".
 */
using System;

namespace InsurancePayments
{
	/// <summary>
	/// Description of Company.
	/// </summary>
	public interface Company
	{
		string[] getKeys();
		string getKey(int index);
		string[] getValues();
		string getValue(string key);
		
	}
}
