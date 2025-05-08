using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Retail.Infrastructure.DataServices;
using Retail.Infrastructure.Hepler;
using Retail.Infrastructure.Models;
using Retail.Infrastructure.RequestModels;
using Retail.Infrastructure.ResponseModels;
using APIResponse = Retail.Infrastructure.ResponseModels.APIResponse;

namespace Retail.Infrastructure.ServiceLayer
{
    public class UserManagementSL
    {
        public async Task<ReceiveContext<PersonLoginResponse>> ValidateUser(LoginRequest loginRequest)
        {
            return await WebServiceUtils<ReceiveContext<PersonLoginResponse>>.PostData(ServiceEndPoints.Login, loginRequest);
        }

        public async Task<ReceiveContext<PersonLoginResponse>> ConcurrentLogin(LoginInputModel loginRequest)
        {
            return await WebServiceUtils<ReceiveContext<PersonLoginResponse>>.PostData(ServiceEndPoints.ConcurrentLogin, loginRequest);
        }

        public async Task<APIResponse> SavePersonProfilePicture(long PersonId, FilesToUpload filesToUpload)
        {
            return await WebServiceUtils<APIResponse>.PutData(ServiceEndPoints.SavePersonProfilePicture + PersonId, filesToUpload);
        }
        //SavePersonProfilePicture
        public async Task<ReceiveContext<PersonLoginResponse>> Register(PersonRegisterRequest person)
        {
            return await WebServiceUtils<ReceiveContext<PersonLoginResponse>>.PostData(ServiceEndPoints.Register, person);
        }
        public async Task<ReceiveContext<string>> ChangePassword(PasswordRequestModel password)
        {
            return await WebServiceUtils<ReceiveContext<string>>.PostData(ServiceEndPoints.ChangePassword, password);
        }
        public async Task<ReceiveContext<PersonLoginResponse>> ForgotPassword(ForgotPasswordRequest email)
        {
            return await WebServiceUtils<ReceiveContext<PersonLoginResponse>>.PostData(ServiceEndPoints.ForgotPasswordOTP, email);
        }
        public async Task<ValidateTokenResponse> UserTokenValidate(UserTokenValidateReq userToken)
        {
            return await WebServiceUtils<ValidateTokenResponse>.PostData(ServiceEndPoints.UserToken, userToken);
        }
        public async Task<ValidateTokenResponse> VerifyEmailMobileTokenForgetPassword(UserTokenValidateReq userToken)
        {
            return await WebServiceUtils<ValidateTokenResponse>.PostData(ServiceEndPoints.VerifyEmailMobileTokenForgetPassword, userToken);
        }
        public async Task<APIResponse> ResendOTP(string UserId)
        {
            return await WebServiceUtils<APIResponse>.PostData(ServiceEndPoints.ResendOTP + UserId, null);
        }
        public async Task<APIResponse> UpdatePersonDetails(PersonUpdateRequest personUpdate)
        {
            return await WebServiceUtils<APIResponse>.PutData(ServiceEndPoints.UpdatePersonDetails + personUpdate.PersonId, personUpdate);
        }

        public async Task<List<PersonNotificationResponse>> GetUnreadNotifications(long PersonId)
        {
            return await WebServiceUtils<List<PersonNotificationResponse>>.GetData(ServiceEndPoints.GetUnreadNotifications + PersonId);
        }
        public async Task<bool> UpdateNotificationReadDate(long PersonId, int NotificationId)
        {
            return await WebServiceUtils<bool>.PutData(string.Format(ServiceEndPoints.UpdateNotificationReadDate, PersonId, NotificationId), null);
        }
        public async Task<ReceiveContext<string>> GetWarrantyCard(long modelId)
        {
            return await WebServiceUtils<ReceiveContext<string>>.PostData(ServiceEndPoints.GetWarrantyCard + modelId, null);
        }
        ///

        public async Task<APIResponse> ForgotPasswordEmail(ForgotPasswordRequest email)
        {
            var result= await WebServiceUtils<APIResponse>.PostData(ServiceEndPoints.ForgotPassword, email);
            return result;
        }
    }
}
