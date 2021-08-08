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

namespace Data.Request.V1.Auth
{
    public class LogoutDataRequest : IRequest<LogoutDataRequestResponse>
    {
        public LogoutDataRequest(int userId)
        {
            UserId = userId;
        }

        public int UserId { get; }
    }

    public class LogoutDataRequestHandler : IRequestHandler<LogoutDataRequest, LogoutDataRequestResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly IRepository<RefreshToken> _refreshTokenRepository;
        
        public LogoutDataRequestHandler(IUserRepository userRepository, IRepository<RefreshToken> refreshTokenRepository)
        {
            _userRepository = userRepository;
            _refreshTokenRepository = refreshTokenRepository;
        }

        public async Task<LogoutDataRequestResponse> Handle(LogoutDataRequest request, CancellationToken cancellationToken)
        {
            var tokens = await _refreshTokenRepository.Where(t => t.UserId == request.UserId);
            var refreshTokens = tokens.ToList();
            
            if (!refreshTokens.Any())
                return null;
            
            _refreshTokenRepository.RemoveRange(refreshTokens);

            var response = new LogoutDataRequestResponse
            {
                IsSuccess = true
            };

            return response;
        }
    }
    
    public class LogoutDataRequestResponse
    {
        public bool IsSuccess { get; set; }
    }
}