using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DocumentFlow.Models
{
    public class IncomingDocumentModel
    {

        /// <summary>
        /// Идентификатор документа
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Дата добавления документа
        /// </summary>
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd'/'MM'/'yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Дата рождения")]
        public DateTime Date { get; set; }
        /// <summary>
        /// Индекс документа
        /// </summary>
        public string DocIndex { get; set; }
        /// <summary>
        /// Краткое описание (содержание) документа
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Автор резолюции
        /// </summary>
        public string Author { get; set; }
        /// <summary>
        /// Резолюция руководителя
        /// </summary>
        public string LeadResolution { get; set; }
        /// <summary>
        /// Срок хранения документа, дней
        /// </summary>
        public int SaveTime { get; set; }
        /// <summary>
        /// Документ (ссылка на документ)
        /// </summary>
        public string DocumentFile { get; set; }
        /// <summary>
        /// Статус документа
        /// </summary>
        public string Status { get; set; }
    }
}