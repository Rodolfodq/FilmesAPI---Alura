using AutoMapper;
using FilmesAPI.Data;
using FilmesAPI.Data.Dtos.Sessao;
using FilmesAPI.Model;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace FilmesAPI.Services
{
    public class SessaoApplicationServices
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        public SessaoApplicationServices(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Sessao> AdicionarSessao(CreateSessaoDto dto)
        {
            Sessao sessao = _mapper.Map<Sessao>(dto);
            await _context.AddAsync(sessao);
            await _context.SaveChangesAsync();
            return sessao;
        }

        public async Task<ReadSessaoDto> RecuperaSessoesPorId(int id)
        {
            Sessao sessao = await _context.Sessoes.FirstOrDefaultAsync(x => x.Id == id);
            if (sessao == null)
                return null;
            ReadSessaoDto dto = _mapper.Map<ReadSessaoDto>(sessao);
            return dto;
        }

    }
}
