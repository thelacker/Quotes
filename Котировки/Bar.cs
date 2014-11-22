using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;


namespace StockQuotes
{
    // класс свечи
    class Bar 
    {
        private double      _oPrice;        // цена открытия свечи
        private double      _cPrice;        // цена закрытия свечи
        private double      _minP;          // минимальная цена
        private double      _maxP;          // максимальная цена
        private TimeSpan    _tSlot;         // временной интервал
        private DateTime    _sTime;         // время начала свечи
        public double       openPrice       // свойство открытия свечи      
        {
            get
            {
                if (_oPrice != null)
                    return _oPrice;
                else
                    return 0;
            }
            set
            {
                _oPrice = value;
            }
        }
        public double       closePrice      // свойство закрытия свечи      
        {
            get
            {
                if (_cPrice != null)
                    return _cPrice;
                else
                    return 0;
            }
            set
            {
                _cPrice = value;
            }
        }
        public double       minPrice        // свойство минимальной цены    
        {
            get
            {
                if (_minP != null)
                    return _minP;
                else
                    return 0;
            }
            set
            {
                _minP = value;
            }
        }
        public double       maxPrice        // свойство максимальной цены   
        {
            get
            {
                if (_maxP != null)
                    return _maxP;
                else
                    return 0;
            }
            set
            {
                _maxP = value;
            }
        }
        public TimeSpan     timeslot        // свойство временного интервала
        {
            get
            {
                return _tSlot;
            }
            set
            {
                _tSlot = value;
            }
        }
        public DateTime     starttime       // свойство начала свечи      
        {
            get
            {
                return _sTime;
            }
            set
            {
                _sTime = value;
            }
        }

        // конструктор класса
        public Bar(double oP, double cP, double maxP, double minP, TimeSpan ts, DateTime st)
        {
            _oPrice = oP;
            _cPrice = cP;
            _maxP = maxP;
            _minP = minP;
            _tSlot = ts;
            _sTime = st;
        }
        // rконструктор пустого объекта
        public Bar()
        {
            _oPrice = 0;
            _cPrice = 0;
            _maxP = 0;
            _minP = 0;
            _tSlot = new TimeSpan();
            _sTime = new DateTime();
        }
    }
}
