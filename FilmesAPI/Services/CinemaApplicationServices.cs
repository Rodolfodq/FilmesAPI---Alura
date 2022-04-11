using AutoMapper;
using FilmesAPI.Data;
using FilmesAPI.Data.Dtos.Cinema;
using FilmesAPI.Model;
using FluentResults;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmesAPI.Services
{
    public class CinemaApplicationServices
    {
        private AppDbContext _context;
        private IMapper _mapper;

        public CinemaApplicationServices(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<int> AdicionaCinema(Cinema cinema)
        {
            await _context.Cinemas.AddAsync(cinema);
            await _context.SaveChangesAsync();
            return cinema.Id;
        }

        public async Task<IEnumerable<ReadCinemaDto>> RecuperaCinemas(string nomeDoFilme)
        {
            List<Cinema> cinemas = await _context.Cinemas.ToListAsync();
            if (cinemas == null)
                return null;
            if (!string.IsNullOrEmpty(nomeDoFilme))
            {
                IEnumerable<Cinema> query = from cinema in cinemas
                                   where cinema.Sessoes.Any(sessao => 
                                   sessao.Filme.Titulo == nomeDoFilme)
                                   select cinema;
                cinemas = query.ToList();
            }
            List<ReadCinemaDto> readDto = _mapper.Map<List<ReadCinemaDto>>(cinemas);
            return readDto;

        }

        public async Task<ReadCinemaDto> RecuperaCinemasPorId(int id)
        {
            Cinema cinema = await _context.Cinemas.FirstOrDefaultAsync(x => x.Id == id);
            if (cinema != null)
            {
                ReadCinemaDto cinemaDto = _mapper.Map<ReadCinemaDto>(cinema);
                return cinemaDto;
            }
            return null;                
        }

        public async Task<Result> AtualizaCinema(int id, UpdateCinemaDto cinemaDto)
        {
            Cinema cinema = await _context.Cinemas.FirstOrDefaultAsync(x => x.Id == id);
            if (cinema == null)
            {
                return Result.Fail("Cinema não encontrado");
            }

            _mapper.Map(cinemaDto, cinema);
            await _context.SaveChangesAsync();
            return Result.Ok();
        }

        public async Task<Result> DeletaCinema(int id)
        {
            Cinema cinema = await _context.Cinemas.FirstOrDefaultAsync(x => x.Id == id);
            if (cinema == null)
            {
                return Result.Fail("Cinema não encontrado");
            }
            _context.Remove(cinema);
            await _context.SaveChangesAsync();
            return Result.Ok();
        }
    }
}
