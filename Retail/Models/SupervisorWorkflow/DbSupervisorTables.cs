using System;
using System.Collections.Generic;
using SQLite;
//using Retail.Infrastructure.ResponseModels;

namespace Retail.Models.SupervisorWorkflow
{
    public class DbSupervisorTables
    {
    }

    #region supervisor location
    public class TblLocationResponse
    {
        public int LocationId { get; set; }
        public string LocationCode { get; set; }
        public string LocationStoreName { get; set; }
        public string Area { get; set; }
        public decimal? Longitude { get; set; }
        public decimal? Latitude { get; set; }
        public string City { get; set; }
        public double Distance { get; set; }
        public long AccountId { get; set; }
    }
    #endregion

    #region supervisor visit schedule
    public class TblVisitScheduleResponse
    {
        [PrimaryKey, AutoIncrement]
        public long PVisitScheduleId { get; set; }

        public long PersonId { get; set; }
        public string CreationDate { get; set; }
        public string VisitScheduleDate { get; set; }
        public int VisitScheduleStatusId { get; set; }
        //public string VisitCode { get; set; }
        //public DateTime? CompletionDate { get; set; }

    }

    public class TblVisitScheduleLocationResponse
    {
        [PrimaryKey, AutoIncrement]
        public long PVisitScheduleLocationId { get; set; }
        public long PVisitScheduleId { get; set; }

        //public long VisitScheduleLocationId { get; set; }
        public long VisitScheduleId { get; set; }
        public int LocationId { get; set; }
        public int VisitLocationTotalTaskCount { get; set; } = 0;
        public int VisitLocationTasksStatusId { get; set; }
        public string VisitLocationTasksStatusName { get; set; }
        public string VisitLocationTasksCountStatus { get; set; }
        public int? StatusId { get; set; } = 1;
        public string CreatedDate { get; set; }
        public bool IsNotEditable { get; set; }

        //public TblLocationResponse Location { get; set; }
        //public List<TblVisitLocationTaskResponse> VisitLocationTasks { get; set; }
        public decimal? PersonLocationLongitude { get; set; }
        public decimal? PersonLocationLatitude { get; set; }
        public decimal? ProximityRange { get; set; }

    }

    public class TblVisitLocationTaskResponse
    {
        [PrimaryKey, AutoIncrement]
        public long PVisitLocationTaskId { get; set; }

        //public long VisitLocationTaskId { get; set; }
        public long TaskMasterId { get; set; }
        //public long VisitScheduleLocationId { get; set; }
        public long PVisitScheduleLocationId { get; set; }
        public int? TaskStatusId { get; set; }
        public DateTime? TaskCompletionDate { get; set; }
        public long? AssignedToPersonId { get; set; }
        public string CreatedDate { get; set; }

        //public TblTaskMasterResponse TaskMaster { get; set; }
        //public TblVisitScheduleLocationResponse VisitScheduleLocation { get; set; }
        //public List<TblTaskQuestionAnswerResponse> TaskQuestionAnswers { get; set; }
    }

    public class TblTaskMasterResponse
    {
        public long TaskMasterId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        //public int? DivisionId { get; set; }
        public bool? IsActive { get; set; }
        public int SerialNo { get; set; }
        //public List<TblTaskMasterQuestionResponse> TaskMasterQuestions { get; set; }
    }

    public class TblTaskQuestionAnswerResponse
    {
        //public TblQuestionMasterResponse QuestionMaster { get; set; }
        //public long TaskQuestionAnswerId { get; set; }
        [PrimaryKey, AutoIncrement]
        public long PTaskQuestionAnswerId { get; set; }
        public long PVisitLocationTaskId { get; set; } //TblVisitLocationTaskResponse

        public string AnswerText { get; set; }
        public string AnswerSign { get; set; }
        public string AnswerBarcode { get; set; }
        public bool? AnswerYesNo { get; set; }
        public int? AnswerNumber { get; set; }
        public DateTime? AnswerDate { get; set; }
        public decimal? AnswerPrice { get; set; }
        public string AnswerQrcode { get; set; }
        public string AnswerCurrency { get; set; }
        public long? AnswerSelectedOptionId { get; set; }
        public string AnswerSelectedOptionText { get; set; }
        public int? StatusId { get; set; }
        public long? AnswerEntryByPersonId { get; set; }
        public DateTime AnswerEntryDate { get; set; }
        public long VisitLocationTaskId { get; set; }
        public long QuestionMasterId { get; set; }
        public long TaskMasterId { get; set; }

        [Ignore]
        public bool? AnswerNo { get; set; }

        //public List<TblAnswerSelectedOptionResponse> AnswerSelectedOptions { get; set; }
        //public List<TblAnswerUploadedFileResponse> AnswerUploadedFiles { get; set; }
    }

    public class TblTaskMasterQuestionResponse
    {
        public long TaskQuestionId { get; set; }
        public long TaskMasterId { get; set; }
        public long QuestionMasterId { get; set; }

        //public TblQuestionMasterResponse QuestionMaster { get; set; }
    }

    public class TblQuestionMasterResponse
    {
        public long QuestionMasterId { get; set; }
        public string QuestionTitle { get; set; }
        public string Description { get; set; }
        public int AnswerTypeId { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsMandatory { get; set; }

        //public List<TblAnswerOptionResponse> AnswerOptions { get; set; }
    }

    public class TblAnswerOptionResponse
    {
        public long OptionId { get; set; }
        public string OptionName { get; set; }
        public bool OptionSelected { get; set; }
        public long QuestionMasterId { get; set; }
    }

    public class TblAnswerSelectedOptionResponse
    {
        public long OptionId { get; set; }
        public long PTaskQuestionAnswerId { get; set; }
        public string OptionName { get; set; }
        public long QuestionMasterId { get; set; }
    }

    public class TblAnswerUploadedFileResponse
    {
        public long PFileInfoId { get; set; }
        public long PTaskQuestionAnswerId { get; set; }
        public long AnswerUploadedFilesId { get; set; }

        //public long TaskQuestionAnswerId { get; set; }
        //public long FileInfoId { get; set; }
        //public TblFileInfoResponse FileInfo { get; set; }
    }

    public class TblFileInfoResponse
    {
        [PrimaryKey,AutoIncrement]
        public long PFileInfoId { get; set; }

        public int FileInfoId { get; set; }
        public string FileName { get; set; }
        public int? FileTypeId { get; set; }
        public string MimeType { get; set; }
        public string Path { get; set; }
        public string Base64StringImage { get; set; }
    }


    public class TblTaskMasterStoreLinkAccount
    {
        public long AccountId { get; set; }
        public long TaskMasterId { get; set; }
    }

    #endregion

}
