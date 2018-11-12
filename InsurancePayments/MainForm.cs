/*
 * Created by SharpDevelop.
 * User: LowPerformance
 * Date: 11.03.2015
 * Time: 15:19
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Windows.Forms;


namespace InsurancePayments
{
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	public partial class MainForm : Form
	{
		static double sum1 = 0, sum2 = 0,sum3;
		static DateTime d0,d1;
		static DataGridViewRowCollection rows;
		static double comissions=0;	//	Сумма комиссий 
		static double shtrafs=0;		//	Сумма штрафов
		static double sms=0;			//	Сумма СМС
		static double others=0;
		string lastFolder="";
		int calFormsCount=0;
		Cal cal;
		public MainForm()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
			test();
		}
		
		// Метод, осуществляющий рассчет
		void racchet()
		{
			rows= table.Rows;	//	Вытаскиваем строки из таблицы
			DataGridViewCellCollection dgvc;	//	Коллекция ячеек строки
			DateTime date;	//	Столбец Дата
			double sumCell;	// 	Сумма изъятых средств
			int countDays;	// 	Количество дней
			double procent;	//	Процент 
			double sumRow = 0;	//	Сумма процентов
			double sumSred = 0;	//	Сумма изъятых средств
			double sumUsch=0;	//	Сумма ущерба по строке
			double sumUschAll=0;	//	Сумма ущерба по всей таблице
			double months;
			double stavka,bankStavka;
			comissions=0;
			shtrafs=0;
			sms=0;
			others=0;
			// Фиксируем введенные значения
			fixup();
			// Импортируем введенные данные в переменные
			label2.Text="0.0";
			label5.Text="0.0";
			label12.Text="0.0";
			label9.Text="0.0";
			if (!Double.TryParse(cProcent.Text.ToString(), out stavka)) {
				MessageBox.Show("Введена ставка рефинансирования ЦБ РФ,которая не является числом", "Ошибка");
				fixdown();
				return;
			}
			if (!Double.TryParse(tBankProcent.Text.ToString(), out bankStavka)) {
				MessageBox.Show("Введена ставка банка,которая не является числом", "Ошибка");
				fixdown();
				return;
			}
			
			if (rows.Count <= 1) {	//	Если не введены строки таблицы 1
				MessageBox.Show("Отсутствуют данные в таблице");
				fixdown();
				return;
			}
			for (int i = 0; i < rows.Count - 1; i++) {
				// Берем каждую строку таблицы
				dgvc = rows[i].Cells;	// Возвращает коллекцию ячеек выбранной строки
				
				// Если в поле ДАТА не введены или введены данные неподходящего типа
				if ((dgvc[0].Value == null) || !DateTime.TryParse(dgvc[0].Value.ToString(), out date)) {	
					MessageBox.Show("Неверно введена дата", "Ошибка");
					fixdown();
					return;
				}
				// Если в поле СУММА ИЗЪЯТЫХ СРЕДСТВ не введена сумма или тип введенных данных не верен, то
				if ((dgvc[1].Value == null) || !Double.TryParse(dgvc[1].Value.ToString(), out sumCell)) {
					MessageBox.Show("Неверно введена сумма изъятых средств", "Ошибка");
					fixdown();
					return;
				}
				if (rows[i].Cells[2].Value!=null){
					if (rows[i].Cells[2].Value.ToString()=="Страховка"){comissions+=sumCell;}
					if (rows[i].Cells[2].Value.ToString()=="Штраф"){shtrafs+=sumCell;}
					if (rows[i].Cells[2].Value.ToString()=="СМС"){sms+=sumCell;}				
					if (rows[i].Cells[2].Value.ToString()=="Прочие"){others+=sumCell;}
				}
				if (i==0) d0=date;
				
				// !!!!!!!!!!!!!!!!!!!
				countDays = calcDays(date,dtpCalcDate.Value);
				procent = (sumCell / 360) * stavka * 0.01 * countDays;	// Процент за пользование чужими средствами
				procent = ((double)Math.Round(procent * 100)) / 100;	// Округляем с точностью до 0.01 руб.
				// !!!!!!!!!!!!!!!!!!!
				
				sumRow += procent;
				sumSred += sumCell;
				if (table2.Rows.Count<table.Rows.Count) table2.Rows.Add();
				// Рассчитываем количество месяцев
				months=Math.Round(((double)countDays)*10/30)*0.1;
				// Рассчитываем сумму ущерба
				double bS=Math.Round(bankStavka/12*100)/100;
				sumUsch=sumCell*bS*0.01*months;
				
				sumUsch=Math.Round(sumUsch*100)/100;
				sumUschAll+=sumUsch;
				
				dgvc[3].Value = countDays.ToString();	//	Вставляем в поле КОЛИЧЕСТВО ДНЕЙ полученное значение
				dgvc[4].Value = procent.ToString();	//	Вставляем в поле ПРОЦЕНТ полученное значение
				table2.Rows[i].Cells[0].Value=date.ToShortDateString();
				table2.Rows[i].Cells[1].Value=sumCell;
				table2.Rows[i].Cells[2].Value=months;	// Количество месяцев
				table2.Rows[i].Cells[3].Value=sumUsch; // Сумма ущерба
				
			}
			// Присваиваем значение текстовому полю 
			label5.Text = sumRow.ToString();	// Сумма процентов
			label2.Text = sumSred.ToString();	// Сумма изъятых средств
			d1=dtpCalcDate.Value;
			label12.Text=(Math.Round(sumUschAll*100)/100).ToString();	// Сумма ущерба
			sum1 = sumRow;
			sum2=sumUschAll;
			sum3=sum1+sum2+sumSred;
			label9.Text=(Math.Round(sum3*100)/100).ToString();
			// Убираем фиксацию введенных значений
			fixdown();			
		}
		void fixup()
		{
			cProcent.Enabled = false;
			dtpBeginContract.Enabled = false;
			dtpCalcDate.Enabled = false;
			dtpLastContract.Enabled = false;
		}
		void fixdown()
		{
			cProcent.Enabled = true;
			dtpBeginContract.Enabled = true;
			dtpCalcDate.Enabled = true;
			dtpLastContract.Enabled = true;
		}
		void clearData()
		{
			tFIO.Clear();
			tContract.Clear();
			cProcent.Text="";
			tBankProcent.Clear();
			tPassport.Clear();
			cBankName.Text = "";
			dtpBeginContract.Value = DateTime.Now;
			dtpCalcDate.Value = DateTime.Now;
			dtpLastContract.Value = DateTime.Now;
			tAddress.Clear();
			tNomer.Clear();
		}
		void clearTable1()
		{
			table.Rows.Clear();
			
		}
		void clearTable2()
		{
			table2.Rows.Clear();
		}
		void BRacchetClick(object sender, EventArgs e)
		{
			racchet();
		}
		void BClearAllDataClick(object sender, EventArgs e)
		{
			clearData();
			clearTable1();
			clearTable2();			
		}
		void BClearTableDataClick(object sender, EventArgs e)
		{
			clearTable1();
		}
		void Button2Click(object sender, EventArgs e)
		{
			clearTable2();
		}
		
		int calcDays(DateTime a, DateTime b)
		{
			return (1+360 * (b.Year - a.Year) + 30 * (b.Month - a.Month) + (b.Day - a.Day));
		}
		double calcMonths(DateTime a, DateTime b)
		{			
			return ((double)Math.Round(((double)calcDays(a, b) / 30) * 10)) / 10;
		}
		// действия при нажатии на кнопку Сохранить
		void BSaveClick(object sender, EventArgs e)
		{
			saveResult();
		}
		void saveResult(){
			// открываем файл
			string tt;
			tt = "<html>" +
			"<head><meta charset=utf-8>" +
			"<title>Расчеты суммы процентов</title>" +
			"</head>" +
			"<body>" +
			"<table border=1 width=100%>" +
			"<tr> <td>Клиент</td><td>" + tFIO.Text.ToString() + "</td></tr>" +
			"<tr> <td>Паспортные данные</td><td>" + tPassport.Text.ToString() + "</td></tr>" +
			"<tr> <td>Адрес регистрации</td><td>" + tAddress.Text.ToString() + "</td></tr>" +
			"<tr> <td>Номер договора услуг</td><td>" + tNomer.Text.ToString() + "</td></tr>" +
			"<tr> <td>Банк</td><td>" + cBankName.Text.ToString() + "  №" + tContract.Text.ToString() + "</td></tr>" +
			"<tr> <td>Дата открытия кредита</td><td>" + dtpBeginContract.Value.ToShortDateString() + " г.</td></tr>" +
			"<tr> <td>Процентная ставка</td><td>" + tBankProcent.Text.ToString() + "%</td></tr>" +
			"<tr> <td>Дата последнего платежа</td><td>" + dtpLastContract.Value.ToShortDateString() + " г.</td></tr>" +
			"</table>" + "<p><p>" +
			"<div align=center><h2>Результаты расчета</h2></div>" +
			"<table border=1 width=100%>" +
			"<tr> <td>Наименование расчета</td><td>Сумма</td></tr>" +
			"<tr> <td>Использованные банком чужие средства</td><td></td></tr>" +
			"<tr> <td>Сумма процентов за пользование чужими средствами</td><td>" + label5.Text.ToString() + "</td></tr>" +
			"<tr> <td>Сумма ущерба с учетом банковского процента по кредиту</td><td>" + label12.Text.ToString() + "</td></tr>" +
			"<tr> <td>Итоговая сумма к возмещению</td><td>" + sum3.ToString() + "</td></tr>" +
			"</table>" +
			"<p><p>" +
			"<div align=center><h2>Сумма процентов за пользование чужими средствами</h2></center>" +
			"<table><tr><td></td><td></td><td></td><td></td></tr></table></div>" +
			"</body>" +
			"</html>";
			String name,name1;
			if (String.IsNullOrEmpty(tFIO.Text.ToString())) {
				// открыть диалоговое окно сохранения файла
				SaveFileDialog sfd = new SaveFileDialog();
				if (sfd.ShowDialog()==DialogResult.OK){
					name = sfd.FileName + " с "+dtpBeginContract.Value.ToShortDateString()+" по "+dtpCalcDate.Value.ToShortDateString()+".html";
				name1=sfd.FileName+" с "+dtpBeginContract.Value.ToShortDateString()+" по "+dtpCalcDate.Value.ToShortDateString()+"_полный_расчет.html";
				} else {
					return;
				}
			} else {
				FolderBrowserDialog fbd = new FolderBrowserDialog();
				if (lastFolder!=""){
					fbd.SelectedPath=lastFolder;
				}
				if (fbd.ShowDialog() == DialogResult.OK) {
					name = fbd.SelectedPath + "\\" + tFIO.Text.ToString() + " с "+dtpBeginContract.Value.ToShortDateString()+" по "+dtpCalcDate.Value.ToShortDateString()+".html";
					name1 = fbd.SelectedPath + "\\" + tFIO.Text.ToString() + " с "+dtpBeginContract.Value.ToShortDateString()+" по "+dtpCalcDate.Value.ToShortDateString()+"_полный_расчет.html";
					lastFolder=fbd.SelectedPath;
				} else {
					return;
				}
			}
			if (!System.IO.File.Exists(name)){
			System.IO.File.WriteAllText(@name, tt);
			} else {MessageBox.Show("Файл с таким именем уже существует"); return;}
			// Создаем таблицу с полным расчетом
			String rTable="<html><head><meta charset=utf-8><title>Расчет исковой расчетной суммы</title></head>" +
				"<body><div align=center><h3>Расчет исковой расчетной суммы за период с "+d0.ToShortDateString()+" г. по " 
				+d1.ToShortDateString()+" г.</h3></div><p><p><table border=1 width=100%><tr><td>Дата списания</td><td>Сумма списания</td>" +
				"<td>Наименование платежа</td><td>Количество дней</td><td>Количество месяцев</td><td>Сумма процентов за использование " +
				"чужих денежных средств</td><td>Сумма ущерба с учетом банковского процента</td></tr>";
			rows= table.Rows;
			// Таблица расчета - Данные 
            for (int i=0;i<rows.Count-1;i++){
				string a="";
				if (rows[i].Cells[2].Value==null) {a="";
				}else {
					a=rows[i].Cells[2].Value.ToString();
				}
				rTable+=resultRow(DateTime.Parse(rows[i].Cells[0].Value.ToString()).ToShortDateString(),rows[i].Cells[1].Value.ToString(),
				                  rows[i].Cells[3].Value.ToString(),table2.Rows[i].Cells[2].Value.ToString(),rows[i].Cells[4].Value.ToString(),
				                  table2.Rows[i].Cells[3].Value.ToString(),a);
			}
			rTable+=resultRow("Итого: ",label2.Text.ToString(),"-","-",label5.Text.ToString(),label12.Text.ToString(),"-");
			rTable+="</table>";
			//rTable+="Платежи: <br>";			
			/*	
			if (comissions!=0) rTable+="<p>Страховки: "+comissions.ToString()+" рублей";
			if (shtrafs!=0) rTable+="<p>Штрафы: "+shtrafs.ToString()+" рублей";
			if (sms!=0) rTable+="<p>СМС: "+sms.ToString()+" рублей";
			if (others!=0) rTable+="<p>Прочие: "+others.ToString()+" рублей";
			*/
			rTable+="<br>Процент за пользование кредитом (полная стоимость):<br>";
			double ProcentPerMonth=Math.Round((Double.Parse(tBankProcent.Text.ToString())/12)*100)/100;
			rTable+=tBankProcent.Text.ToString()+"% - 1  год,<br>"+ProcentPerMonth+"% - 1 месяц.";
			// Если были комиссии за снятие наличных
			if (comissions!=0){
				rTable+="<p>Расчет по страховке</p><p>Процент за пользование чужими денежными средствами:</p>";
				for (int i=0;i<table.Rows.Count;i++){
					if ((table.Rows[i].Cells[2].Value!=null)&&table.Rows[i].Cells[2].Value.ToString()=="Страховка"){
						rTable+="<p>"+table.Rows[i].Cells[1].Value.ToString()+" руб. х "+table.Rows[i].Cells[3].Value.ToString()+" дней х "+cProcent.Text.ToString()+"% / 360 = "+table.Rows[i].Cells[4].Value.ToString()+" рублей.</p>";
						rTable+="<p>Банковский процент за пользование кредитом на сумму комиссии:</p>";
						rTable+="<p>"+table.Rows[i].Cells[1].Value.ToString()+" руб. х "+table2.Rows[i].Cells[2].Value.ToString()+" месяцев х "+ProcentPerMonth+"% = "+table2.Rows[i].Cells[3].Value.ToString()+" рублей.</p>";
						break;
					}
				}
			}
			
			
			
			// Если были комиссии за подключение к программе страхования
			if (shtrafs!=0){
				rTable+="<p>Расчет штрафов</p><p>Процент за пользование чужими денежными средствами:</p>";
				for (int i=0;i<table.Rows.Count;i++){
					if ((table.Rows[i].Cells[2].Value!=null)&&table.Rows[i].Cells[2].Value.ToString()=="Штраф"){
						rTable+="<p>"+table.Rows[i].Cells[1].Value.ToString()+" руб. х "+table.Rows[i].Cells[3].Value.ToString()+" дней х "+cProcent.Text.ToString()+"% / 360 = "+table.Rows[i].Cells[4].Value.ToString()+" рублей.</p>";
						rTable+="<p>Банковский процент за пользование кредитом на сумму комиссии:</p>";
						rTable+="<p>"+table.Rows[i].Cells[1].Value.ToString()+" руб. х "+table2.Rows[i].Cells[2].Value.ToString()+" месяцев х "+ProcentPerMonth+"% = "+table2.Rows[i].Cells[3].Value.ToString()+" рублей.</p>";
						break;
					}
				}
			}
			
			// Если были комиссии за использование СМС-услуг
			if (sms!=0){
				rTable+="<p>Расчет комиссии за использование СМС-услуг</p><p>Процент за пользование чужими денежными средствами:</p>";
				for (int i=0;i<table.Rows.Count;i++){
					if ((table.Rows[i].Cells[2].Value!=null)&&table.Rows[i].Cells[2].Value.ToString()=="СМС"){
						rTable+="<p>"+table.Rows[i].Cells[1].Value.ToString()+" руб. х "+table.Rows[i].Cells[3].Value.ToString()+" дней х "+cProcent.Text.ToString()+"% / 360 = "+table.Rows[i].Cells[4].Value.ToString()+" рублей.</p>";
						rTable+="<p>Банковский процент за пользование кредитом на сумму комиссии:</p>";
						rTable+="<p>"+table.Rows[i].Cells[1].Value.ToString()+" руб. х "+table2.Rows[i].Cells[2].Value.ToString()+" месяцев х "+ProcentPerMonth+"% = "+table2.Rows[i].Cells[3].Value.ToString()+" рублей.</p>";
						break;
					}
				}
			}
			// Прочие комиссии
			if (others!=0){
				rTable+="<p>Расчет прочих комиссий</p><p>Процент за пользование чужими денежными средствами:</p>";
				for (int i=0;i<table.Rows.Count;i++){
					if ((table.Rows[i].Cells[2].Value!=null)&&table.Rows[i].Cells[2].Value.ToString()=="Прочие"){
						rTable+="<p>"+table.Rows[i].Cells[1].Value.ToString()+" руб. х "+table.Rows[i].Cells[3].Value.ToString()+" дней х "+cProcent.Text.ToString()+"% / 360 = "+table.Rows[i].Cells[4].Value.ToString()+" рублей.</p>";
						rTable+="<p>Банковский процент за пользование кредитом на сумму комиссии:</p>";
						rTable+="<p>"+table.Rows[i].Cells[1].Value.ToString()+" руб. х "+table2.Rows[i].Cells[2].Value.ToString()+" месяцев х "+ProcentPerMonth+"% = "+table2.Rows[i].Cells[3].Value.ToString()+" рублей.</p>";
						break;
					}
				}
			}
			
			double x=0;
			// Расчет неустойки за пользование денежными средствами
				// Если дата подачи претензии меньше на и более 10 дней, чем текущая дата,то
				// рассчитываем неустойку
				
				if (calcDays(dtpLastContract.Value,dtpCalcDate.Value)>=10){
					rTable+="<p>Расчет неустойки за неудовлетворение претензии</p>";
					double a=Double.Parse(label2.Text.ToString());
					double b=calcDays(dtpLastContract.Value,dtpCalcDate.Value);
					double c=(double)Math.Round(a*3*b)/100;
					rTable+="<p>"+a+" x 3% x "+b+" дней (с "+dtpLastContract.Value.ToShortDateString()+" по "+dtpCalcDate.Value.ToShortDateString()+")= "+c+" рублей";
					if (c>a){
						rTable+=",</p>";
						rTable+="<p>но не более, чем "+a+" рублей.</p>";
						c=a;
						x+=c;
					} else {
						rTable+=".</p>";
					}
				}
				// В противном случае, неустойку не рассчитываем
			//
			
			
			rTable+="<p>Моральный ущерб - 5000 рублей.</p>";
			x+=Double.Parse(label2.Text.ToString())+Double.Parse(label5.Text.ToString())+Double.Parse(label12.Text.ToString());
			x+=5000;
			rTable+="<p>Итого (без учета штрафа): "+x+" рублей.</p>";
			double y=(double)Math.Round(x*50)/100;
			rTable+="<p>Штраф 50% от суммы: "+y+" рублей.</p>";
			double z=x+y;
			rTable+="<p>Всего: "+z+" рублей.</p>";
			rTable+="<p>Расчет выполнил: </p>";
			rTable+="</body></html>";	
			
			System.IO.File.WriteAllText(@name1,rTable);
			MessageBox.Show("Сохранение завершено");
		}
		void MainFormKeyPress(object sender, KeyPressEventArgs e)
		{			
			if (e.KeyChar.Equals(Keys.F1)) {
				MessageBox.Show("Разработка: Дмитрий Лазарев <lazarevdmitry2008@gmail.com>");
			}
		}
		string resultRow(String a, String b, String c, String d, String e, String f, String g){
			return "<tr>" +
				"<td>"+ a+"</td>" +
				"<td>"+ b+"</td>" +
				"<td>"+ g+"</td>" +
				"<td>"+ c+"</td>" +
				"<td>"+ d+"</td>" +
				"<td>"+ e+"</td>" +
				"<td>"+ f+"</td>" +
				"</tr>";			
		}
		void Button1Click(object sender, EventArgs e)
		{
			download();
		}
		void download(){
			SaveFileDialog  sfd = new SaveFileDialog();
			sfd.ShowDialog();
			if (sfd.CheckFileExists) {
				MessageBox.Show("Файл с таким именем уже существует");
			} else {
				string outText="";
				outText+="FIO:";
				if (tFIO.Text!=null){outText+=tFIO.Text.ToString()+"\n";}else {outText+=" \n";}
				outText+="NDOGOVOR:";
				if (tContract.Text!=null){outText+=tContract.Text.ToString()+"\n";}else {outText+=" \n";}
				outText+="CBSTAVKA:";
				if (cProcent.SelectedValue!=null){outText+=cProcent.SelectedValue.ToString()+"\n";}else {outText+=" \n";}
				outText+="BSTAVKA:";
				if (tBankProcent.Text!=null){outText+=tBankProcent.Text.ToString()+"\n";}else {outText+=" \n";}
				outText+="PASSPORT:";
				if (tPassport.Text!=null){outText+=tPassport.Text.ToString()+"\n";}else {outText+=" \n";}
				outText+="BANK:";
				if (cBankName.Text!=null){outText+=cBankName.Text.ToString()+"\n";}else {outText+=" \n";}
				outText+="DATESTART:";
				outText+=dtpBeginContract.Value.ToShortDateString()+"\n";
				outText+="DATEEND:";
				outText+=dtpLastContract.Value.ToShortDateString()+"\n";
				outText+="DATEPRETENTION:";
				outText+=dtpCalcDate.Value.ToShortDateString()+"\n";
				outText+="ADDRESS:";
				if (tAddress.Text!=null){outText+=tAddress.Text.ToString()+"\n";}else {outText+=" \n";}
				outText+="ACONTRACT:";
				if (tNomer.Text!=null){outText+=tNomer.Text.ToString()+"\n";}else {outText+=" \n";}
				outText+="TABLE:\n";
				for (int i=0;i<table.Rows.Count-1;i++){
					if (table.Rows[i].Cells[0].Value!=null){outText+="COL0:"+table.Rows[i].Cells[0].Value.ToString()+"\n";}else {outText+="COL0:\n";}
					if (table.Rows[i].Cells[1].Value!=null){outText+="COL1:"+table.Rows[i].Cells[1].Value.ToString()+"\n";}else {outText+="COL1:\n";}
					if (table.Rows[i].Cells[2].Value!=null){outText+="COL2:"+table.Rows[i].Cells[2].Value.ToString()+"\n";}else {outText+="COL2:\n";}
					if (table.Rows[i].Cells[3].Value!=null){outText+="COL3:"+table.Rows[i].Cells[3].Value.ToString()+"\n";}else {outText+="COL3:\n";}
					if (table.Rows[i].Cells[4].Value!=null){outText+="COL4:"+table.Rows[i].Cells[4].Value.ToString()+"\n";}else {outText+="COL4:\n";}
					if (table2.Rows[i].Cells[2].Value!=null){outText+="COL5:"+table2.Rows[i].Cells[2].Value.ToString()+"\n";}else {outText+="COL5:\n";}
					if (table2.Rows[i].Cells[3].Value!=null){outText+="COL6:"+table2.Rows[i].Cells[3].Value.ToString()+"\n";}else {outText+="COL6:\n";}
										
				}
				if (sfd.FileName.Length!=0){
				System.IO.File.WriteAllText(@sfd.FileName,outText);
				MessageBox.Show("Сохранение завершено");
				}
			}
		
		}
		void MainFormLoad(object sender, EventArgs e)
		{
			
		}
		// Кнопка ЗАГРУЗКА
		void Button3Click(object sender, EventArgs e)
		{
			upload();
		}
		void upload(){
		// Запрашиваем у пользователя, действительно ли он хочет очистить форму приложения
			DialogResult dr=MessageBox.Show("Вы действительно хотите очистить окно приложения?","",MessageBoxButtons.YesNo);
			if (dr.HasFlag(DialogResult.No)){
				return;
			}
			clearData();
			clearTable1();
			clearTable2();
			// Выводим окно выбора файла
			OpenFileDialog fd = new OpenFileDialog();
			fd.ShowDialog();
			// Проверяем наличие файла.
			// Если файл существует, то загружаем.
			// Если файл не существует, то выводим сообщение.
			if (System.IO.File.Exists(fd.FileName))
			{
				// После загрузки файла читаем его в массив строк
				String[] lines=System.IO.File.ReadAllLines(fd.FileName);
				// Разбираем строки, выводя данные в окно программы
				tFIO.Text=lines[0].Split(':')[1];
				tContract.Text=lines[1].Split(':')[1];
				cProcent.SelectedValue=lines[2].Split(':')[1];
				tBankProcent.Text=lines[3].Split(':')[1];
				tPassport.Text=lines[4].Split(':')[1];
				cBankName.Text=lines[5].Split(':')[1];
				dtpBeginContract.Value=DateTime.Parse(lines[6].Split(':')[1]);
				dtpLastContract.Value=DateTime.Parse(lines[7].Split(':')[1]);
				dtpCalcDate.Value=DateTime.Parse(lines[8].Split(':')[1]);
				tAddress.Text=lines[9].Split(':')[1];
				tNomer.Text=lines[10].Split(':')[1];
				
				int j=0;
							
				try {
				for (int i=12;i<lines.Length-1;i=i+7){
					table.Rows.Add();
					table2.Rows.Add();
					table.Rows[j].Cells[0].Value=lines[i].Split(':')[1];
					table.Rows[j].Cells[1].Value=lines[i+1].Split(':')[1];
					table.Rows[j].Cells[2].Value=lines[i+2].Split(':')[1];
					table.Rows[j].Cells[3].Value=lines[i+3].Split(':')[1];
					table.Rows[j].Cells[4].Value=lines[i+4].Split(':')[1];
					
					table2.Rows[j].Cells[0].Value=lines[i].Split(':')[1];
					table2.Rows[j].Cells[1].Value=lines[i+1].Split(':')[1];
					table2.Rows[j].Cells[2].Value=lines[i+5].Split(':')[1];
					table2.Rows[j].Cells[3].Value=lines[i+6].Split(':')[1];
					
					j++;
				}		
				} catch (System.ArgumentOutOfRangeException ex){
					MessageBox.Show(ex.Message);
				}
				MessageBox.Show("Загрузка завершена");
				
			}
			else {
				MessageBox.Show("Нет файла с таким именем");
				return;
			}		
		}
		// При изменении размера окна
		void MainFormResize(object sender, EventArgs e)
		{
	
		}
		void AboutProgramToolStripMenuItemClick(object sender, EventArgs e)
		{
			MessageBox.Show("Разработка: Дмитрий Лазарев <lazarevdmitry2008@gmail.com>","О программе",MessageBoxButtons.OK,MessageBoxIcon.Information,MessageBoxDefaultButton.Button1);
		}
		void ВыходToolStripMenuItemClick(object sender, EventArgs e)
		{
			this.Dispose();
		}
		void ЗагрузитьДанныеToolStripMenuItemClick(object sender, EventArgs e)
		{
			upload();
		}
		void СохранитьДанныеToolStripMenuItemClick(object sender, EventArgs e)
		{
			download();
		}
		void СохранитьРезультатToolStripMenuItemClick(object sender, EventArgs e)
		{
			saveResult();
		}
		void ВырезатьToolStripMenuItemClick(object sender, EventArgs e)
		{
			
		}
		void РассчитьПретензиюToolStripMenuItemClick(object sender, EventArgs e)
		{
			this.Enabled=false;
			Pretention pretention=new Pretention();
			pretention.Activate();
			pretention.Show();
			this.Enabled=true;
			
		}
		void TableCellContentClick(object sender, DataGridViewCellEventArgs e)
		{
	
		}
	
		void test(){
			
		}
		void LCalcDateClick(object sender, EventArgs e)
		{
	
		}
		void TableCellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
		{
		
			/*
			//DataGridView d0 = (DataGridView)sender;
			DataGridViewCell d= table.Rows[e.RowIndex].Cells[e.ColumnIndex];
			//MessageBox.Show(e.ColumnIndex.ToString());
			if (e.ColumnIndex==0&&calFormsCount==0){
				//calFormsCount++;
				cal= new Cal(ref d);
				cal.Left=d.ContentBounds.Left+50;
				cal.Top=d.ContentBounds.Bottom+50;
				cal.Activate();
				cal.Visible=true;
				while (cal!=null){
					
				}
			}
			*/
		}
		
		
	}
	
}
