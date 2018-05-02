using System;
using System.Threading.Tasks;
using LanguageExt;
using Newtonsoft.Json;

namespace Infusio.Http
{
    using static Prelude;
    using static Formatting;
    using static JsonConvert;

    public static class HttpSupport
    {
        public static Task<Either<InfusioError, InfusioResult<T>>> interpret<T>(InfusioOp<T> op, InfusioClient client) =>
            RunWith(op, InterpreterState.Empty, client);

        public static Task<Either<InfusioError, InfusioResult<T>>> RunWith<T>(this InfusioOp<T> op, InfusioClient client) =>
            RunWith(op, InterpreterState.Empty, client);

        public static Task<Either<InfusioError, InfusioResult<T>>> interpret<T>(InfusioOp<T> op, InterpreterState state, InfusioClient client) =>
            RunAsync(op, state, client).ToEither();

        public static Task<Either<InfusioError, InfusioResult<T>>> RunWith<T>(this InfusioOp<T> op, InterpreterState state, InfusioClient client) =>
            RunAsync(op, state, client).ToEither();

        static EitherAsync<InfusioError, InfusioResult<T>> RunAsync<T>(InfusioOp<T> op, InterpreterState state, InfusioClient client) =>
            op is InfusioOp<T>.Return r ? Right<InfusioError, InfusioResult<T>>(new InfusioResult<T>(r.Value, state.Logs)).ToAsync() :
            op is InfusioOp<T>.Log l ? Right<InfusioError, InfusioResult<T>>(new InfusioResult<T>(default, state.Log(l.Message).Logs)).ToAsync() :
            op is InfusioOp<T>.GetAccountProfile _1 ? Exe(op.ToString(), () => client.GetAccountProfile(), _1.Next, state, client) :
            op is InfusioOp<T>.UpdateAccountInfo _2 ? Exe(op.ToString(), () => client.UpdateAccountInfo(_2.AccountInfo), _2.Next, state, client) :
            op is InfusioOp<T>.SearchCommissions _3 ? Exe(op.ToString(), () => client.SearchCommissions(_3.AffiliateId, _3.Offset, _3.Limit, _3.Until, _3.Since), _3.Next, state, client) :
            op is InfusioOp<T>.RetrieveAffiliateModel _4 ? Exe(op.ToString(), () => client.RetrieveAffiliateModel(), _4.Next, state, client) :
            op is InfusioOp<T>.ListAppointments _5 ? Exe(op.ToString(), () => client.ListAppointments(_5.ContactId, _5.Offset, _5.Limit, _5.Until, _5.Since), _5.Next, state, client) :
            op is InfusioOp<T>.CreateAppointment _6 ? Exe(op.ToString(), () => client.CreateAppointment(_6.Appointment), _6.Next, state, client) :
            op is InfusioOp<T>.RetrieveAppointmentModel _7 ? Exe(op.ToString(), () => client.RetrieveAppointmentModel(), _7.Next, state, client) :
            op is InfusioOp<T>.GetAppointment _8 ? Exe(op.ToString(), () => client.GetAppointment(_8.AppointmentId), _8.Next, state, client) :
            op is InfusioOp<T>.UpdateAppointment _9 ? Exe(op.ToString(), () => client.UpdateAppointment(_9.AppointmentDTO, _9.AppointmentId), _9.Next, state, client) :
            op is InfusioOp<T>.DeleteAppointment _10 ? Exe(op.ToString(), () => client.DeleteAppointment(_10.AppointmentId), _10.Next, state, client) :
            op is InfusioOp<T>.UpdatePropertiesOnAppointment _11 ? Exe(op.ToString(), () => client.UpdatePropertiesOnAppointment(_11.AppointmentDTO, _11.AppointmentId), _11.Next, state, client) :
            op is InfusioOp<T>.ListCampaigns _12 ? Exe(op.ToString(), () => client.ListCampaigns(_12.OrderDirection, _12.Order, _12.SearchText, _12.Offset, _12.Limit), _12.Next, state, client) :
            op is InfusioOp<T>.GetCampaign _13 ? Exe(op.ToString(), () => client.GetCampaign(_13.CampaignId, _13.OptionalProperties), _13.Next, state, client) :
            op is InfusioOp<T>.AddContactsToCampaignSequence _14 ? Exe(op.ToString(), () => client.AddContactsToCampaignSequence(_14.Ids, _14.SequenceId, _14.CampaignId), _14.Next, state, client) :
            op is InfusioOp<T>.RemoveContactsFromCampaignSequence _15 ? Exe(op.ToString(), () => client.RemoveContactsFromCampaignSequence(_15.Ids, _15.SequenceId, _15.CampaignId), _15.Next, state, client) :
            op is InfusioOp<T>.AddContactToCampaignSequence _16 ? Exe(op.ToString(), () => client.AddContactToCampaignSequence(_16.ContactId, _16.SequenceId, _16.CampaignId), _16.Next, state, client) :
            op is InfusioOp<T>.RemoveContactFromCampaignSequence _17 ? Exe(op.ToString(), () => client.RemoveContactFromCampaignSequence(_17.ContactId, _17.SequenceId, _17.CampaignId), _17.Next, state, client) :
            op is InfusioOp<T>.ListCompanies _18 ? Exe(op.ToString(), () => client.ListCompanies(_18.OptionalProperties, _18.OrderDirection, _18.Order, _18.CompanyName, _18.Offset, _18.Limit), _18.Next, state, client) :
            op is InfusioOp<T>.CreateCompany _19 ? Exe(op.ToString(), () => client.CreateCompany(_19.Company), _19.Next, state, client) :
            op is InfusioOp<T>.RetrieveCompanyModel _20 ? Exe(op.ToString(), () => client.RetrieveCompanyModel(), _20.Next, state, client) :
            op is InfusioOp<T>.ListContacts _21 ? Exe(op.ToString(), () => client.ListContacts(_21.OrderDirection, _21.Order, _21.FamilyName, _21.GivenName, _21.Email, _21.Offset, _21.Limit), _21.Next, state, client) :
            op is InfusioOp<T>.CreateContact _22 ? Exe(op.ToString(), () => client.CreateContact(_22.Contact), _22.Next, state, client) :
            op is InfusioOp<T>.CreateOrUpdateContact _23 ? Exe(op.ToString(), () => client.CreateOrUpdateContact(_23.Contact), _23.Next, state, client) :
            op is InfusioOp<T>.RetrieveContactModel _24 ? Exe(op.ToString(), () => client.RetrieveContactModel(), _24.Next, state, client) :
            op is InfusioOp<T>.DeleteContact _25 ? Exe(op.ToString(), () => client.DeleteContact(_25.ContactId), _25.Next, state, client) :
            op is InfusioOp<T>.UpdatePropertiesOnContact _26 ? Exe(op.ToString(), () => client.UpdatePropertiesOnContact(_26.ContactId, _26.Contact), _26.Next, state, client) :
            op is InfusioOp<T>.CreateCreditCard _27 ? Exe(op.ToString(), () => client.CreateCreditCard(_27.ContactId, _27.CreditCard), _27.Next, state, client) :
            op is InfusioOp<T>.ListEmailsForContact _28 ? Exe(op.ToString(), () => client.ListEmailsForContact(_28.ContactId, _28.Email, _28.ContactId2, _28.Offset, _28.Limit), _28.Next, state, client) :
            op is InfusioOp<T>.CreateEmailForContact _29 ? Exe(op.ToString(), () => client.CreateEmailForContact(_29.ContactId, _29.EmailWithContent), _29.Next, state, client) :
            op is InfusioOp<T>.ListAppliedTags _30 ? Exe(op.ToString(), () => client.ListAppliedTags(_30.ContactId, _30.Offset, _30.Limit), _30.Next, state, client) :
            op is InfusioOp<T>.ApplyTagsToContactId _31 ? Exe(op.ToString(), () => client.ApplyTagsToContactId(_31.TagIds, _31.ContactId), _31.Next, state, client) :
            op is InfusioOp<T>.RemoveTagsFromContact _32 ? Exe(op.ToString(), () => client.RemoveTagsFromContact(_32.Ids, _32.ContactId), _32.Next, state, client) :
            op is InfusioOp<T>.RemoveTagsFromContact2 _33 ? Exe(op.ToString(), () => client.RemoveTagsFromContact2(_33.TagId, _33.ContactId), _33.Next, state, client) :
            op is InfusioOp<T>.GetContact _34 ? Exe(op.ToString(), () => client.GetContact(_34.Id, _34.OptionalProperties), _34.Next, state, client) :
            op is InfusioOp<T>.ListEmails _35 ? Exe(op.ToString(), () => client.ListEmails(_35.Email, _35.ContactId, _35.Offset, _35.Limit), _35.Next, state, client) :
            op is InfusioOp<T>.CreateEmail _36 ? Exe(op.ToString(), () => client.CreateEmail(_36.EmailWithContent), _36.Next, state, client) :
            op is InfusioOp<T>.CreateEmails _37 ? Exe(op.ToString(), () => client.CreateEmails(_37.EmailWithContent), _37.Next, state, client) :
            op is InfusioOp<T>.DeleteEmails _38 ? Exe(op.ToString(), () => client.DeleteEmails(_38.EmailIds), _38.Next, state, client) :
            op is InfusioOp<T>.GetEmail _39 ? Exe(op.ToString(), () => client.GetEmail(_39.Id), _39.Next, state, client) :
            op is InfusioOp<T>.UpdateEmail _40 ? Exe(op.ToString(), () => client.UpdateEmail(_40.Id, _40.EmailWithContent), _40.Next, state, client) :
            op is InfusioOp<T>.DeleteEmail _41 ? Exe(op.ToString(), () => client.DeleteEmail(_41.Id), _41.Next, state, client) :
            op is InfusioOp<T>.ListFiles _42 ? Exe(op.ToString(), () => client.ListFiles(_42.Name, _42.Type, _42.Permission, _42.Viewable, _42.Offset, _42.Limit), _42.Next, state, client) :
            op is InfusioOp<T>.CreateFile _43 ? Exe(op.ToString(), () => client.CreateFile(_43.FileUpload), _43.Next, state, client) :
            op is InfusioOp<T>.GetFile _44 ? Exe(op.ToString(), () => client.GetFile(_44.FileId, _44.OptionalProperties), _44.Next, state, client) :
            op is InfusioOp<T>.UpdateFile _45 ? Exe(op.ToString(), () => client.UpdateFile(_45.FileId, _45.FileUpload), _45.Next, state, client) :
            op is InfusioOp<T>.DeleteFile _46 ? Exe(op.ToString(), () => client.DeleteFile(_46.FileId), _46.Next, state, client) :
            op is InfusioOp<T>.ListStoredHookSubscriptions _47 ? Exe(op.ToString(), () => client.ListStoredHookSubscriptions(), _47.Next, state, client) :
            op is InfusioOp<T>.CreateAHookSubscription _48 ? Exe(op.ToString(), () => client.CreateAHookSubscription(_48.RestHookRequest), _48.Next, state, client) :
            op is InfusioOp<T>.ListHookEventTypes _49 ? Exe(op.ToString(), () => client.ListHookEventTypes(), _49.Next, state, client) :
            op is InfusioOp<T>.RetrieveAHookSubscription _50 ? Exe(op.ToString(), () => client.RetrieveAHookSubscription(_50.Key), _50.Next, state, client) :
            op is InfusioOp<T>.UpdateAHookSubscription _51 ? Exe(op.ToString(), () => client.UpdateAHookSubscription(_51.RestHookRequest, _51.Key), _51.Next, state, client) :
            op is InfusioOp<T>.DeleteAHookSubscription _52 ? Exe(op.ToString(), () => client.DeleteAHookSubscription(_52.Key), _52.Next, state, client) :
            op is InfusioOp<T>.VerifyAHookSubscriptionDelayed _53 ? Exe(op.ToString(), () => client.VerifyAHookSubscriptionDelayed(_53.XHookSecret, _53.Key), _53.Next, state, client) :
            op is InfusioOp<T>.VerifyAHookSubscription _54 ? Exe(op.ToString(), () => client.VerifyAHookSubscription(_54.Key), _54.Next, state, client) :
            op is InfusioOp<T>.GetUserInfo _55 ? Exe(op.ToString(), () => client.GetUserInfo(), _55.Next, state, client) :
            op is InfusioOp<T>.ListOpportunities _56 ? Exe(op.ToString(), () => client.ListOpportunities(_56.Order, _56.SearchTerm, _56.StageId, _56.UserId, _56.Offset, _56.Limit), _56.Next, state, client) :
            op is InfusioOp<T>.CreateOpportunity _57 ? Exe(op.ToString(), () => client.CreateOpportunity(_57.Opportunity), _57.Next, state, client) :
            op is InfusioOp<T>.UpdateOpportunity _58 ? Exe(op.ToString(), () => client.UpdateOpportunity(_58.Opportunity), _58.Next, state, client) :
            op is InfusioOp<T>.RetrieveOpportunityModel _59 ? Exe(op.ToString(), () => client.RetrieveOpportunityModel(), _59.Next, state, client) :
            op is InfusioOp<T>.GetOpportunity _60 ? Exe(op.ToString(), () => client.GetOpportunity(_60.OpportunityId, _60.OptionalProperties), _60.Next, state, client) :
            op is InfusioOp<T>.UpdatePropertiesOnOpportunity _61 ? Exe(op.ToString(), () => client.UpdatePropertiesOnOpportunity(_61.OpportunityId, _61.Opportunity), _61.Next, state, client) :
            op is InfusioOp<T>.ListOpportunityStagePipelines _62 ? Exe(op.ToString(), () => client.ListOpportunityStagePipelines(), _62.Next, state, client) :
            op is InfusioOp<T>.ListOrders _63 ? Exe(op.ToString(), () => client.ListOrders(_63.ProductId, _63.ContactId, _63.Order, _63.Paid, _63.Offset, _63.Limit, _63.Until, _63.Since), _63.Next, state, client) :
            op is InfusioOp<T>.RetrieveOrderModel _64 ? Exe(op.ToString(), () => client.RetrieveOrderModel(), _64.Next, state, client) :
            op is InfusioOp<T>.GetOrder _65 ? Exe(op.ToString(), () => client.GetOrder(_65.OrderId), _65.Next, state, client) :
            op is InfusioOp<T>.ListTransactionsForOrder _66 ? Exe(op.ToString(), () => client.ListTransactionsForOrder(_66.OrderId, _66.ContactId, _66.Offset, _66.Limit, _66.Until, _66.Since), _66.Next, state, client) :
            op is InfusioOp<T>.ListProducts _67 ? Exe(op.ToString(), () => client.ListProducts(_67.Active, _67.Offset, _67.Limit), _67.Next, state, client) :
            op is InfusioOp<T>.ListProductsFromSyncToken _68 ? Exe(op.ToString(), () => client.ListProductsFromSyncToken(_68.Offset, _68.Limit, _68.SyncToken), _68.Next, state, client) :
            op is InfusioOp<T>.GetProduct _69 ? Exe(op.ToString(), () => client.GetProduct(_69.ProductId), _69.Next, state, client) :
            op is InfusioOp<T>.GetApplicationEnabled _70 ? Exe(op.ToString(), () => client.GetApplicationEnabled(), _70.Next, state, client) :
            op is InfusioOp<T>.GetContactOptionTypes _71 ? Exe(op.ToString(), () => client.GetContactOptionTypes(), _71.Next, state, client) :
            op is InfusioOp<T>.RetrieveSubscriptionModel _72 ? Exe(op.ToString(), () => client.RetrieveSubscriptionModel(), _72.Next, state, client) :
            op is InfusioOp<T>.ListTags _73 ? Exe(op.ToString(), () => client.ListTags(_73.Category, _73.Offset, _73.Limit), _73.Next, state, client) :
            op is InfusioOp<T>.CreateTag _74 ? Exe(op.ToString(), () => client.CreateTag(_74.Tag), _74.Next, state, client) :
            op is InfusioOp<T>.CreateTagCategory _75 ? Exe(op.ToString(), () => client.CreateTagCategory(_75.TagCategory), _75.Next, state, client) :
            op is InfusioOp<T>.GetTag _76 ? Exe(op.ToString(), () => client.GetTag(_76.Id), _76.Next, state, client) :
            op is InfusioOp<T>.ListContactsForTagId _77 ? Exe(op.ToString(), () => client.ListContactsForTagId(_77.TagId, _77.Offset, _77.Limit), _77.Next, state, client) :
            op is InfusioOp<T>.ApplyTagToContactIds _78 ? Exe(op.ToString(), () => client.ApplyTagToContactIds(_78.Ids, _78.TagId), _78.Next, state, client) :
            op is InfusioOp<T>.RemoveTagFromContactIds _79 ? Exe(op.ToString(), () => client.RemoveTagFromContactIds(_79.Ids, _79.TagId), _79.Next, state, client) :
            op is InfusioOp<T>.RemoveTagFromContactId _80 ? Exe(op.ToString(), () => client.RemoveTagFromContactId(_80.ContactId, _80.TagId), _80.Next, state, client) :
            op is InfusioOp<T>.ListTasks _81 ? Exe(op.ToString(), () => client.ListTasks(_81.Order, _81.Offset, _81.Limit, _81.Completed, _81.Until, _81.Since, _81.UserId, _81.HasDueDate, _81.ContactId), _81.Next, state, client) :
            op is InfusioOp<T>.CreateTask _82 ? Exe(op.ToString(), () => client.CreateTask(_82.Task), _82.Next, state, client) :
            op is InfusioOp<T>.RetrieveTaskModel _83 ? Exe(op.ToString(), () => client.RetrieveTaskModel(), _83.Next, state, client) :
            op is InfusioOp<T>.ListTasksForCurrentUser _84 ? Exe(op.ToString(), () => client.ListTasksForCurrentUser(_84.Order, _84.Offset, _84.Limit, _84.Completed, _84.Until, _84.Since, _84.UserId, _84.HasDueDate, _84.ContactId), _84.Next, state, client) :
            op is InfusioOp<T>.GetTask _85 ? Exe(op.ToString(), () => client.GetTask(_85.TaskId), _85.Next, state, client) :
            op is InfusioOp<T>.UpdateTask _86 ? Exe(op.ToString(), () => client.UpdateTask(_86.Task, _86.TaskId), _86.Next, state, client) :
            op is InfusioOp<T>.DeleteTask _87 ? Exe(op.ToString(), () => client.DeleteTask(_87.TaskId), _87.Next, state, client) :
            op is InfusioOp<T>.UpdatePropertiesOnTask _88 ? Exe(op.ToString(), () => client.UpdatePropertiesOnTask(_88.Task, _88.TaskId), _88.Next, state, client) :
            op is InfusioOp<T>.ListTransactions _89 ? Exe(op.ToString(), () => client.ListTransactions(_89.ContactId, _89.Offset, _89.Limit, _89.Until, _89.Since), _89.Next, state, client) :
            op is InfusioOp<T>.GetTransaction _90 ? Exe(op.ToString(), () => client.GetTransaction(_90.TransactionId), _90.Next, state, client) :
            throw new NotSupportedException();

        static EitherAsync<InfusioError, InfusioResult<B>> Exe<T, B>(string describe, Func<Task<Either<InfusioError, T>>> fn, Func<T, InfusioOp<B>> nextOp, InterpreterState state, InfusioClient client) =>
            from right in LogOperation(describe, state, fn).ToAsync()
            from next in RunAsync(nextOp(right.Value), right.State, client)
            select next;

        static Task<Either<InfusioError, (InterpreterState State, T Value)>> LogOperation<T>(string describe, InterpreterState state, Func<Task<Either<InfusioError, T>>> fn) =>
            from st in Right<InfusioError, (InterpreterState, Unit)>((state.Log(describe), unit)).AsTask()
            from result in fn().ToAsync().Match(
                Left: error => Left<InfusioError, (InterpreterState, T)>(error),
                Right: value => Right<InfusioError, (InterpreterState, T)>((st.Item1.Log($" -> {SerializeObject(value, Indented)}"), value))
            )
            select result;
    }

    public class InterpreterState
    {
        public static readonly InterpreterState Empty = new InterpreterState(Seq<string>());
        public readonly Seq<string> Logs;

        public InterpreterState(Seq<string> logs) =>
            Logs = logs;

        public InterpreterState Log(string message) =>
            new InterpreterState(Logs.Append(Seq1(message)));
    }
}