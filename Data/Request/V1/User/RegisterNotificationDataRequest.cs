using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Core.Interface;
using Core.Model;
using Core.Model.Base;
using Core.Security;
using MediatR;

namespace Data.Request.V1.User
{
    public class RegisterNotificationDataRequest : IRequest<RegisterNotificationDataRequestResponse>
    {
        public RegisterNotificationDataRequest(int userId, string fcmToken, string apnsToken)
        {
            UserId = userId;
            FcmToken = fcmToken;
            ApnsToken = apnsToken;
        }

        public int UserId { get; }
        public string FcmToken { get; }
        public string ApnsToken { get; }
    }

    public class RegisterNotificationDataRequestHandler : IRequestHandler<RegisterNotificationDataRequest, RegisterNotificationDataRequestResponse>
    {
        private readonly IUserRepository _userRepository;

        public RegisterNotificationDataRequestHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<RegisterNotificationDataRequestResponse> Handle(RegisterNotificationDataRequest request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(request.UserId);

            if (user == null)
                return null;

            user.FcmToken = request.FcmToken;
            user.ApnsToken = request.ApnsToken;

            try
            {
                _userRepository.Update(user);
                return new RegisterNotificationDataRequestResponse {IsSuccess = true};
            }
            catch (Exception e)
            {
                return new RegisterNotificationDataRequestResponse {IsSuccess = false};
            }
        }
    }
    
    public class RegisterNotificationDataRequestResponse
    {
        public bool IsSuccess { get; set; }
    }
}