using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DocumentFlow.Models
{
    public class OutboxDocumentModel
    {

        /// <summary>
        /// Идентификатор документа
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Дата добавления документа
        /// </summary>
        [DataType(DataType.Date)]
        //[DisplayFormat(DataFormatString = "{0:dd'/'MM'/'yyyy}", ApplyFormatInEditMode = true)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Дата добавления документа")]
        public DateTime Date { get; set; }
        /// <summary>
        /// Индекс документа
        /// </summary>
        /// 
        [Display(Name = "Индекс документа")]
        public string DocIndex { get; set; }
        /// <summary>
        /// Краткое описание (содержание) документа
        /// </summary>
        [Display(Name = "Краткое описание")]
        public string Description { get; set; }
        /// <summary>
        /// Автор резолюции
        /// </summary>
        [Display(Name = "Автор резолюции")]
        public string Author { get; set; }
        /// <summary>
        /// Резолюция руководителя
        /// </summary>
        [Display(Name = "Резолюция руководителя")]
        public string LeadResolution { get; set; }
        /// <summary>
        /// Резолюция руководителя
        /// </summary>
        [Display(Name = "Логин пользователя")]
        public string LeadResolutionLogin { get; set; }
        /// <summary>
        /// Срок хранения документа, до даты
        /// </summary>
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Срок хранения")]
        public DateTime SaveTime { get; set; }
        /// <summary>
        /// Документ (ссылка на документ)
        /// </summary>
        [Display(Name = "Документ")]
        public string DocumentFile { get; set; }
        /// <summary>
        /// Примечание к документу
        /// </summary>
        [Display(Name = "Примечание")]
        public string NoteToDocument { get; set; }
        /// <summary>
        /// Статус документа
        /// </summary>
        [Display(Name = "Статус")]
        public string Status { get; set; }
    }
}