using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rent.Utility
{
    public static class StaticDetails
    {
        public const string AdminRole = "Админ";
        public const string ClientRole = "Клиент";
        public const string RepairManRole = "Мастер по ремонту";
        public const string StoreKeeperRole = "Кладовщик";


		public const string StatusConfirmed = "Подтверждено";
		public const string StatusInProcess = "Подготовка инструмента";
		public const string StatusReady = "Готово к выдаче";
		public const string StatusIssued = "Выдано клиенту";
		public const string StatusReturned = "Возвращено клиентом";
		public const string StatusCompleted = "Завершено";
		public const string StatusCancelled = "Отменено";
	}
}
