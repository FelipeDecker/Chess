# TODO - Chess

Análise do estado atual do projeto (console C#/.NET 10) e sugestões de próximos passos.

## O que já está pronto
- Movimentação completa das 6 peças, com regras corretas de cada uma.
- Roque (kingside e queenside), en passant e promoção de peão.
- Detecção de xeque, prevenção de auto-xeque e xeque-mate.
- Interface colorida no console, com destaque de movimentos possíveis e peças capturadas.

## TODO - para o projeto ficar "completo" como jogo de xadrez
1. **Empate (draw)** — hoje só existe xeque-mate; faltam:
   - Afogamento (stalemate).
   - Regra dos 50 lances sem captura/movimento de peão.
   - Tripla repetição de posição.
   - Material insuficiente para dar mate (ex: Rei x Rei, Rei+Bispo x Rei).
2. **Validação de entrada** — tratar entradas mal formatadas (ex: "z9", texto vazio) sem quebrar o loop; hoje `ChessPosition` pode lançar exceção não tratada.
3. **Desfazer jogada (undo) pelo usuário** — o motor já suporta `UndoMove` internamente (usado para validar xeque), mas não existe comando para o jogador desfazer um lance.
4. **Registro da partida (notação/histórico)**:
   - Gravar lances em notação algébrica (PGN) ou notação simples (ex: `e2e4`).
   - Permitir salvar/carregar partida em arquivo (retomar depois).
5. **Empate por acordo / desistência (resign)** — comando para encerrar a partida manualmente.
6. **Cronômetro / relógio de xadrez** (opcional, mas comum em "completar o jogo").
7. **Testes automatizados** — não há projeto de testes (xUnit/NUnit). Cobrir:
   - Regras especiais (roque, en passant, promoção).
   - Casos de xeque-mate e afogamento conhecidos.
   - Casos de borda (roque bloqueado, passar por casa atacada, etc).
8. **Refatorações pequenas**:
   - `Match.MakeMove` calcula `VulnerableEnPassant` **depois** de já ter avançado o turno/trocar jogador — revisar ordem de execução (o en passant é setado após `ChangePlayer()`, mas antes seria mais claro).
   - Extrair mensagens de erro para constantes/localização, facilitando tradução (o README já é bilíngue implícito).
   - Adicionar `IEquatable`/`Equals` customizado em `Piece` se necessário para lógica com `HashSet`.
9. **Configuração de partida** — escolher cor, jogar contra si mesmo, inverter tabuleiro para jogador preto.
10. **Acessibilidade** — mensagens sem depender só de cor (para daltonismo), e teclado mais amigável (setas em vez de digitar coordenadas).

## Ideias - o que fazer com o que já existe (evoluir o projeto)
1. **Separar o "motor" (engine) da interface**
   - Extrair `Board`, `Pieces`, `Match` para uma biblioteca (`Chess.Core`, `.dll`) sem dependência de `Console`.
   - Isso permite reaproveitar a mesma lógica em qualquer front-end (web, desktop, mobile).
2. **Interface gráfica desktop**
   - Criar um app **WPF** ou **Avalonia** (multiplataforma) consumindo a biblioteca do motor.
   - Tabuleiro clicável com drag-and-drop das peças, animações simples, temas de tabuleiro/peças.
3. **Interface web**
   - Backend em **ASP.NET Core Web API** expondo o motor (endpoints para novo jogo, mover peça, estado atual).
   - Frontend em **Blazor** (reaproveitando C#) ou React/JS puro, com tabuleiro em HTML/CSS/SVG.
   - Permite jogar do navegador, inclusive mobile.
4. **Multiplayer online**
   - Usar **SignalR** para sincronizar jogadas entre dois clientes em tempo real (dá para reaproveitar a experiência com Azure, se for o caso).
   - Salas de jogo, matchmaking simples, espectadores.
5. **Inteligência artificial / bot**
   - Implementar um oponente simples com **minimax + poda alfa-beta** e avaliação de material/posição.
   - Evoluir para busca mais profunda, tabelas de abertura, ou até integrar um motor existente via protocolo **UCI** (ex: Stockfish) para comparação.
6. **Exportação/Importação PGN e FEN**
   - Permitir carregar posições famosas (FEN) e analisar, ou importar partidas históricas em PGN para "replay".
7. **Modo de análise**
   - Mostrar avaliação de posição, sugestão de melhor lance, histórico navegável (voltar/avançar lances).
8. **Publicação como pacote/ferramenta**
   - Publicar o motor como pacote NuGet reutilizável.
   - Publicar o app console como ferramenta global do .NET (`dotnet tool install`).
9. **Contêiner e deploy**
   - Empacotar a versão web em Docker e publicar em Azure (App Service ou Container Apps) para jogar de qualquer lugar.
10. **Gamificação**
	- Sistema de ranking (Elo simples), histórico de partidas por jogador, conquistas (ex: "primeiro xeque-mate por afogamento evitado").
</content>
