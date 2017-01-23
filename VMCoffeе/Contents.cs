using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VMCoffeе
{
    
    class Contents
    {
        
        //Количество элементов
        protected int count;

        //Цена элементов
        protected int priсe;

        //***********************************************************//
        
        //Возвращаем количество
        public int Count
        {
            get
            {
            return count; 
            }
        }

        //***********************************************************//
        
        //Возвращаем цену
        public int Price
        {
            get
            {
                return priсe;
            }
        }
        //***********************************************************//
   
        //Конструктор по умолчанию
        public Contents()
        {
            this.count =  0;
            this.priсe =  1;
        }
        //***********************************************************//
   
        //Конструктор с параметрами
        public Contents(int count,int price)
        {
            this.count = (count > 0) ? count : 0;
            this.priсe = (price > 0) ? price : 1;
        }
        //***********************************************************//
   
        //Метод увеличения значения колличества
        public void add(int c)
        {
            count += c;
        }
        //***********************************************************//
   
        /* Метод для уменьшения значения колличества
         * если хватает количество для выдачи то возвращаем 0
         * иначе возвращаем количество которое не хватило
         */
        public int sub(int c)
        {
            count -= c;
            if (count>=0) return 0;
            else
            {
                int buf = count;
                count = 0;
                return Math.Abs(buf);
            }
            
        }
    }
    //***********************************************************//
    
    //Класс для работы с деньгами
    class Money:Contents
    {
        public Money()
            :base()
        {
            
        }
        //***********************************************************//
   
        //Конструктор с параметрами
        public Money(int count,int price)
            :base(count,price)
        {
        }
        //***********************************************************//
   
        /* метод получения монет
         * при удачном завершении true
         * иначе false
         * входные параметры:
         * массив кол-ва монет
         * массив монет
         */
        public static bool getMoney(int[] mon,ref Money[] m)
        {
            if((mon.Length==m.Length))
            {
                for (int i = 0; i < m.Length; i++)
                    m[i].add(mon[i]);
                    return true;
            }
                else
            {
                return false;
            }
        }
        //***********************************************************//
   
        /* метод для сдачи 
         * возвращает массив с количеством монет
         * при невозможности дать всю сдачю возвращает null
         * входные параметры:
         * массив монет для сдачи
         * кол-во сдач
         */
        public static int[] giveMoney(ref Money[] mon, int pric)
        {
            //временная переменная для хранения массива возвращаемых монет
            int[] tmp=new int[mon.Length];
            int buf;
            for (int i = mon.Length - 1; i >= 0;i-- )
            {
                tmp[i] = 0;
                buf = pric / mon[i].Price;
                buf -= mon[i].sub(buf);
                tmp[i] = buf;
                pric -= buf * mon[i].Price;
            }

            if (pric != 0)
            {
                getMoney(tmp,ref mon);
                return null;
            }
            else return tmp;
            
        }
    }
    //***********************************************************//
   
    //Класс для напитков наследуется от Contents
  class Drink:Contents
  {
      //Название напитка
      protected string name;
      
      //Свойство для возврата имени
      public string Name { get { return name; } }
      
      //***********************************************************//
   
      //Конструктор по умолчанию
      public Drink()
          :base()
      {
          name="";
      }
      //***********************************************************//
   
      //Конструктор с параметрами
      public Drink(int count,int price, string name)
          :base(count,price)
      {
          this.name=name;
      }
      //***********************************************************//
   
  }

}
