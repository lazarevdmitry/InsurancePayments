/*
 * Создано в SharpDevelop.
 * Dmitry Lazarev
 * Дата: 29.06.2015
 * Время: 14:23
 * E-mail: lazarevdmitry2008@gmail.com
 * Для изменения этого шаблона используйте меню "Инструменты | Параметры | Кодирование | Стандартные заголовки".
 */
using System;
using System.Drawing;
using System.Windows.Forms;

namespace InsurancePayments
{
	/// <summary>
	/// Description of Cal.
	/// </summary>
	public partial class Cal : Form
	{
		DataGridViewCell x;
		public Cal(ref DataGridViewCell d)
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
			x=d;
		}
		void Button1Click(object sender, EventArgs e)
		{
			x.Value=monthCalendar1.SelectionStart.ToShortDateString();
			this.Dispose();
		}
		void MonthCalendar1DateSelected(object sender, DateRangeEventArgs e)
		{
			x.Value=monthCalendar1.SelectionStart.ToShortDateString();
			this.Dispose();
		}
		
	}
}
