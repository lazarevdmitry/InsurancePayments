/*
 * Создано в SharpDevelop.
 * Dmitry Lazarev
 * Дата: 26.07.2015
 * Время: 18:12
 * E-mail: lazarevdmitry2008@gmail.com
 * Для изменения этого шаблона используйте меню "Инструменты | Параметры | Кодирование | Стандартные заголовки".
 */
using System;
using System.Drawing;
using System.Windows.Forms;

namespace InsurancePayments
{
	/// <summary>
	/// Description of Pretention.
	/// </summary>
	public partial class Pretention : Form
	{
		private string kemVynesen;
		private string FIOSudyi;
		private DateTime kogdaVynesen;
		private string NomerSP;
		private string vPolzuKogo;
		private string FIOOtvetchika;
		private bool pol;
		private string adresOtvetchika;
		private string vidVzyskaniya;
		private string NomerDogovora;
		private double ObschayaSumma;
		private DateTime kogdaPoluchen;
		private bool VosstanovlenieSrokov;
		private string[] Prilozhenie;
		private DateTime dataOtmeny;
		private string[] nazvaniePlatezha;
		private double[] summaPlatezha;
		private string path;
		private String o=" ";
		public Pretention()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
			
		}
		// Кнопка Очистить
		void Button2Click(object sender, EventArgs e)
		{
			clearData();
		}
		void Button1Click(object sender, EventArgs e)
		{
			init();
			
			System.Windows.Forms.SaveFileDialog save= new SaveFileDialog();
			save.Filter="Документ html|*.html";
			save.ShowDialog();
			
			path=save.FileName;				
			openFile();
			
		}
		
		
		// Метод для очистки всех полей 
		private void clearData(){
			//textBox1.Text="";
			comboBox2.SelectedValue="";
			textBox2.Text="";
			dateTimePicker1.Value=DateTime.Now;
			textBox3.Text="";
			textBox4.Text="";
			textBox5.Text="";			
			textBox6.Text="";
			textBox7.Text="";
			textBox8.Text="";
			textBox9.Text="";			
			dateTimePicker2.Value=DateTime.Now;
			radioButton1.Checked=true;
			radioButton2.Checked=false;
				
			
			textBox10.Text="";
			dateTimePicker3.Value=DateTime.Now;
			dataGridView1.Rows.Clear();
		
		}
		private void init(){
			//kemVynesen=textBox1.Text.ToString();
			kemVynesen=comboBox2.SelectedText.ToString();
			FIOSudyi=textBox2.Text.ToString();
			kogdaVynesen=dateTimePicker1.Value;
			NomerSP=textBox3.Text.ToString();
			vPolzuKogo=textBox4.Text.ToString();
			FIOOtvetchika=textBox5.Text.ToString();
			if (comboBox1.SelectedText.ToString()=="Мужчина"){
				pol=true;
			} else {
				pol=false;
			}
			adresOtvetchika=textBox6.Text.ToString();
			vidVzyskaniya=textBox7.Text.ToString();
			NomerDogovora=textBox8.Text.ToString();
			Double.TryParse(textBox9.Text,out ObschayaSumma);
			kogdaPoluchen=dateTimePicker2.Value;
			if (radioButton1.Checked){
				VosstanovlenieSrokov=true;
			} else {
				VosstanovlenieSrokov=false;
			}
			Prilozhenie=textBox10.Text.Split(',');
			dataOtmeny=dateTimePicker3.Value;
			nazvaniePlatezha=new string[dataGridView1.Rows.Count];
			summaPlatezha=new double[dataGridView1.Rows.Count];
			for (int i=0;i<dataGridView1.Rows.Count;i++){
				try {
				nazvaniePlatezha[i]=dataGridView1.Rows[i].Cells[0].Value.ToString();
				} catch(NullReferenceException e){
					Console.Write(e.Message);
				}
				Double.TryParse((string)dataGridView1.Rows[i].Cells[1].Value, out summaPlatezha[i]);
				
			}
		}
		// Метод, в котором произоводятся все основные операции 
		private void openFile(){
			String[] s = new string[1024];
			String temp;
			temp= sudya(kemVynesen,"дательный")+o+fio(FIOSudyi,"дательный");
			s[0]=HTMLConverter.p(HTMLConverter.setOtstup(temp,60));
			temp=FIOOtvetchika+o+adresOtvetchika;
			s[1]=HTMLConverter.p(HTMLConverter.setOtstup(temp,60));
			temp="Возражение";
			s[2]=HTMLConverter.centralize(HTMLConverter.p(temp));
			temp="на судебный приказ";
			s[3]=HTMLConverter.centralize(HTMLConverter.p(temp));
			
			string[] a=new string[20];
			a[0]="вынесен судебный приказ ";
			a[1]="о взыскании с ";
			a[2]="в пользу ";
			a[3]="в размере ";
			a[4]="рублей.";
			a[5]="Я возражаю против исполнения данного судебного приказа.";
			a[6]="Судебный приказ я получил";
			a[7]="На основании вышеизложенного и руководствуясь ст. ст. 128,129, 135 ГПК РФ,";
			a[8]="Прошу:";
			a[9]="Восстановить срок подачи возражения на судебный приказ";
			a[10]="Отменить судебный приказ  ";
			a[11]="Приложение:";
			a[12]="Судебный приказ я получила";
			string sp;
			sp=kogdaVynesen.ToShortDateString()+o+sudya(kemVynesen,"творительный").ToLower()+fio(FIOSudyi,"творительный")+
				o+a[0]+o+"№"+o+NomerSP+o+a[1]+o+fio(FIOOtvetchika,"родительный")+o+vidVzyskaniya+o+a[2]+o+vPolzuKogo+o;
			for (int i=0;i<nazvaniePlatezha.Length;i++){
				sp+=nazvaniePlatezha[i]+o+a[3]+o+summaPlatezha[i]+", ";
				if (i==nazvaniePlatezha.Length-1){
					sp+=" а всего ";
				}
			}
			if (nazvaniePlatezha.Length==0){
				sp+=a[3];
			}
			
			sp+=ObschayaSumma.ToString()+o+a[4];
			temp=sp;
			s[4]=HTMLConverter.p(temp);
			s[5]=HTMLConverter.p(a[5]);
			if (pol){
				temp=a[6]+o+kogdaPoluchen.ToShortDateString();
			} else {temp=a[12]+o+kogdaPoluchen.ToShortDateString();}
			s[6]=HTMLConverter.p(temp);
			s[7]=HTMLConverter.p(a[7]);
			s[8]=HTMLConverter.centralize(HTMLConverter.p(HTMLConverter.b(a[8])));
			int j=1;
			if (VosstanovlenieSrokov){
				temp=j+"."+o+a[9]+o+sp;
				j++;
			}
			s[9]=HTMLConverter.p(temp);
			temp=j+"."+o+a[10]+o+sp;
			s[10]=HTMLConverter.p(temp);
			int x=0;
			if (Prilozhenie.Length>0){
				temp=a[11];
				s[11]=HTMLConverter.p(temp);
				for (int k=0;k<Prilozhenie.Length;k++){
					temp=(k+1).ToString()+o+Prilozhenie[k];
					a[k+11]=HTMLConverter.p(temp);
					x=k+11;
				}				
			}
			temp=dataOtmeny.ToShortDateString()+o+"г."+o+FIOOtvetchika;
			s[x]=HTMLConverter.p(temp);
			
			string html0="<html><head><title></title></head><body>";
			string html1="</body></html>";
			
			string oo=String.Join("",s);
			System.IO.File.AppendAllText(path, html0+oo+html1);
			
		}
		// Метод
		private String sudya(String s, String padezh){
			if (s=="Мировой судья"){
				if (padezh=="дательный"){
					return "Мировому судье";
				}
				if (padezh=="творительный"){
					return "Мировым судьей";
				}
			} else	if (s=="Федеральный судья"){
				if (padezh=="дательный"){
					return "Федеральному судье";
				}
				if (padezh=="творительный"){
					return "Федеральным судьей";
				}
			} else {MessageBox.Show("Не введены данные судьи","Внимание");return "";}
			return "";
			
		}
		private String fio(String s, String padezh){
			if (s.Length<=0) {return "";}
			string[] x=s.Split(' ');
			string f=x[0];
			string i=x[1];
			string ot=x[2];
			
			String[] temp=new string[3];
			if (padezh=="дательный"){
				if (f.EndsWith("ов")||f.EndsWith("ев")){
					temp[0]=f+"у";
				}
				if (f.EndsWith("ова")||f.EndsWith("ева")){
					char[] lettersS=f.ToCharArray();
					char[] lettersF=new char[lettersS.Length+1];
					for (int y=0;y<lettersS.Length-1;y++){
						lettersF[y]=lettersS[y];
					}
					lettersF[lettersS.Length]='о';
					lettersF[lettersS.Length+1]='й';
					temp[0]=lettersF.ToString();
				}
				if (f.EndsWith("ский")){
					char[] letters1=f.ToCharArray();
					char[] letters2=new char[letters1.Length+1];
					for (int y=0;y<letters1.Length-2;y++){
						letters2[y]=letters1[y];
					}
					letters2[letters1.Length-2]='о';
					letters2[letters1.Length-1]='м';
					letters2[letters1.Length]='у';
					temp[0]=letters2.ToString();
				}
				if (f.EndsWith("ская")){
					char[] letters1=f.ToCharArray();
					char[] letters2=new char[letters1.Length];
					for (int y=0;y<letters1.Length-2;y++){
						letters2[y]=letters1[y];
					}
					letters2[letters1.Length-2]='о';
					letters2[letters1.Length-1]='й';
					
					temp[0]=letters2.ToString();
				}
			} else if (padezh=="творительный"){
				if (f.EndsWith("ов")||f.EndsWith("ев")){
					temp[0]=f+"ым";
				}
				if (f.EndsWith("ова")||f.EndsWith("ева")){
					char[] lettersS=f.ToCharArray();
					char[] lettersF=new char[lettersS.Length+1];
					for (int y=0;y<lettersS.Length-1;y++){
						lettersF[y]=lettersS[y];
					}
					lettersF[lettersS.Length]='о';
					lettersF[lettersS.Length+1]='й';
					temp[0]=lettersF.ToString();
				}
				if (f.EndsWith("ский")){
					char[] letters1=f.ToCharArray();
					char[] letters2=new char[letters1.Length];
					for (int y=0;y<letters1.Length-2;y++){
						letters2[y]=letters1[y];
					}
					letters2[letters1.Length-2]='и';
					letters2[letters1.Length-1]='м';
					
					temp[0]=letters2.ToString();
				}
				if (f.EndsWith("ская")){
					char[] letters1=f.ToCharArray();
					char[] letters2=new char[letters1.Length];
					for (int y=0;y<letters1.Length-2;y++){
						letters2[y]=letters1[y];
					}
					letters2[letters1.Length-2]='о';
					letters2[letters1.Length-1]='й';
					
					temp[0]=letters2.ToString();
				}
			} else if (padezh=="родительный"){
				if (f.EndsWith("ов")||f.EndsWith("ев")){
					temp[0]=f+"а";
				}
				if (f.EndsWith("ова")||f.EndsWith("ева")){
					char[] lettersS=f.ToCharArray();
					char[] lettersF=new char[lettersS.Length+1];
					for (int y=0;y<lettersS.Length-1;y++){
						lettersF[y]=lettersS[y];
					}
					lettersF[lettersS.Length]='о';
					lettersF[lettersS.Length+1]='й';
					temp[0]=lettersF.ToString();
				}
				if (f.EndsWith("ский")){
					char[] letters1=f.ToCharArray();
					char[] letters2=new char[letters1.Length+1];
					for (int y=0;y<letters1.Length-2;y++){
						letters2[y]=letters1[y];
					}
					letters2[letters1.Length-2]='о';
					letters2[letters1.Length-1]='г';
					letters2[letters1.Length]='о';
					
					temp[0]=letters2.ToString();
				}
				if (f.EndsWith("ская")){
					char[] letters1=f.ToCharArray();
					char[] letters2=new char[letters1.Length];
					for (int y=0;y<letters1.Length-2;y++){
						letters2[y]=letters1[y];
					}
					letters2[letters1.Length-2]='о';
					letters2[letters1.Length-1]='й';
					
					temp[0]=letters2.ToString();
				}
			}
			// Имя
			string[] Inames={"Александр","Дмитрий","Максим","Даниил","Кирилл", "Ярослав", "Денис", "Никита", "Иван", "Артем",
			"Андрей", "Николай", "Алексей", "Илья", "Владимир", "Евгений", "Константин","Сергей", "Валерий",
			"Матвей","Олег", "Юрий",
			"Елена", "Юлия", "Екатерина", "Наталья", "Мария", "Диана", "Майя", "Виктория", "Анастасия", "Александра",
			"Анна", "Алиса", "Виолетта", "Софья", "Ольга", "Светлана", "Дарья", "Ксения", "Василиса", "Ирина"};
			string[] Dnames={"Александру","Дмитрию","Максиму","Даниилу","Кириллу","Ярославу","Денису","Никите",
			"Ивану","Андрею","Николаю","Алексею","Илье","Владимиру","Евгению","Константину","Сергею","Валерию",
			"Матвею","Олегу","Юрию",
			"Елене","Юлии","Екатерине","Наталье","Марие","Диане","Майе","Виктории","Анастасии","Александре",
			"Анне","Алисе","Виолетте","Софье","Ольге","Светлане","Дарье","Ксении","Василисе","Ирине"};
			string[] Tnames={"Александром","Дмитрием","Максимом","Даниилом","Кириллом","Ярославом","Денисом",
			"Никитой","Иваном","Андреем","Николаем","Алексеем","Ильей","Владимиром","Евгением","Константином",
			"Сергеем","Валерием","Матвеем","Олегом","Юрием","Еленой","Юлией","Екатериной","Натальей","Марией",
			"Дианой","Майей","Викторией","Анастасией","Александрой","Анной","Алисой","Виолеттой","Софьей",
			"Ольгой","Светланой","Дарьей","Ксенией","Василисой","Ириной"};
			if (padezh=="дательный"){
				for (int q=0;q<Inames.Length;q++){
					if (i==Inames[q]){
						temp[1]=Dnames[q];
						break;
					}
				}			
			}
			if (padezh=="творительный"){
				for (int q=0;q<Inames.Length;q++){
					if (i==Inames[q]){
						temp[1]=Tnames[q];
						break;
					}
				}			
			}
			// Отчество
			if (padezh=="дательный"){
				if (ot.EndsWith("вна")){
					char[] a=ot.ToCharArray();
					char[] z=new char[a.Length];
					for (int v=0;v<a.Length-1;v++){
						z[v]=a[v];
					}
					z[z.Length-1]='е';
					temp[2]=z.ToString();
				}
				if (ot.EndsWith("вич")){
					char[] a=ot.ToCharArray();
					char[] z= new char[a.Length+1];
					for (int v=0;v<a.Length;v++){
						z[v]=a[v];
					}
					z[a.Length]='у';
					temp[2]=z.ToString();
				}
			}
			if (padezh=="творительный"){
				if (ot.EndsWith("вна")){
					char[] a=ot.ToCharArray();
					char[] z=new char[a.Length+1];
					for (int v=0;v<a.Length-1;v++){
						z[v]=a[v];
					}
					z[a.Length-1]='о';
					z[a.Length]='й';
					temp[2]=z.ToString();
				}
				if (ot.EndsWith("вич")){
					char[] a=ot.ToCharArray();
					char[] z=new char[a.Length+2];
					for (int v=0;v<a.Length;v++){
						z[v]=a[v];
					}
					z[a.Length]='е';
					z[a.Length+1]='м';
					temp[2]=z.ToString();
				}			
			}
			if (padezh=="родительный"){
				if (ot.EndsWith("вна")){
					char[] a=ot.ToCharArray();
					char[] z=new char[a.Length+1];
					for (int v=0;v<a.Length-1;v++){
						z[v]=a[v];
					}
					z[a.Length-1]='о';
					z[a.Length]='й';
					temp[2]=z.ToString();
				}
				if (ot.EndsWith("вич")){
					char[] a=ot.ToCharArray();
					char[] z=new char[a.Length+1];
					for (int v=0;v<a.Length;v++){
						z[v]=a[v];
					}
					z[a.Length]='а';
					
					temp[2]=z.ToString();
				}			
			}
			return temp[0]+o+temp[1]+o+temp[2];
		}
		void RadioButton1CheckedChanged(object sender, EventArgs e)
		{
			if (radioButton1.Checked){
				radioButton2.Checked=false;
			}
		}
		void RadioButton2CheckedChanged(object sender, EventArgs e)
		{
			if (radioButton2.Checked){
				radioButton1.Checked=false;
			}
		}
	
		// 
	}
}
