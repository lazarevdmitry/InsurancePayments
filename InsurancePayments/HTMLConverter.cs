/*
 * Создано в SharpDevelop.
 * Dmitry Lazarev
 * Дата: 28.07.2015
 * Время: 13:39
 * E-mail: lazarevdmitry2008@gmail.com
 * Для изменения этого шаблона используйте меню "Инструменты | Параметры | Кодирование | Стандартные заголовки".
 */
using System;

namespace InsurancePayments
{
	/// <summary>
	/// Description of HTMLConverter.
	/// </summary>
	public class HTMLConverter
	{
		public HTMLConverter()
		{
		}
		public static String p(String s){
			return "<p>"+s+"</p>";
		}
		
		public static String setOtstup(String s, int percents){
			if (percents>=100) {return s;}
			return "<div style=\" left:"+percents+"%;\">"+s+"</div>";
		}
		public static String centralize(String s){
			return "<div style=\"align:center;\">"+s+"</div>";
		}
		public static String b(String s){
			return "<strong>"+s+"</strong>";
		}
		public static String a(String s, String[] args){
			string[] reqArgs={"href","accesskey","coords","download","hreflang",
								"name","rel","rev","shape","tabindex","target","title","type"};
			return HTMLConverter.temp(s,"a",args,reqArgs);			
		}
		public static String abbr(String s,String arg){
			string[] x={arg};
			string[] y={"title"};
			return HTMLConverter.temp(s,"abbr",x,y);
		}
		public static String abbr(String s){
			string[] x={"abracadabra"};
			string[] y={"none"};
			return HTMLConverter.temp(s,"abbr",x,y);
		}
		public static String acronym(String s){
			string[] x={"abracadabra"};
			string[] y={"none"};
			return HTMLConverter.temp(s,"acronym",x,y);
		}
		public static String address(String s){
			string[] x={"abracadabra"};
			string[] y={"none"};
			return HTMLConverter.temp(s,"address",x,y);
		}
		
		
		
		
		private static String temp(String stroka, String tagName, String[] realArgs,String[] requiredArgs){
			string sp=" ";
			string temp0="<"+tagName+sp;
			string temp1;
			for (int i=0;i<requiredArgs.Length;i++){
				temp1=null;
				for (int j=0;j<realArgs.Length;j++){
					if (realArgs[j].Contains(requiredArgs[i])){
						temp1=realArgs[j];
					}
				}
				if (temp1!=null){temp0+=temp1+sp;}
			}
			temp0+=">"+stroka+"</"+tagName+">";
			return temp0;
		}
	}
}
