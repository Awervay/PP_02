//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PRPROIZV
{
    using System;
    using System.Collections.Generic;
    
    public partial class History_Employee
    {
        public int ID_Employee { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public int ID_Input { get; set; }
        public int ID_History { get; set; }
    
        public virtual Employee Employee { get; set; }
        public virtual Type_input Type_input { get; set; }
    }
}
