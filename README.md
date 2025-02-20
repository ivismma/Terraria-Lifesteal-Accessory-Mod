# Terraria Lifesteal Accessory Mod
Um mod para o Terraria que adiciona um novo item no jogo: **Vampire Claw**<br>

Esse mod foi desenvolvido com o uso da biblioteca [tModLoader](https://store.steampowered.com/app/1281930/tModLoader/).
O tModLoader é um projeto [open-source](https://github.com/tModLoader/tModLoader) que permite não só o uso de mods para o Terraria como também fácil acesso/download de mods para jogadores e uma 
boa documentação que auxilia no desenvolvimento para modders.

A ideia no desenvolvimento do mod partiu do fato de que o Terraria só possui dois itens que oferecem roubo de vida, apesar de ter mais de 3.000 itens no jogo.<br>
O atributo "roubo de vida" não existe no jogo, o que torna esse recurso bem raro.
Esse mod oferece o uso desse acessório como se fosse mais um "atributo" do personagem, permitindo uma grande variedade de "builds" e sendo possível combinar com diversas armas do jogo.<br>

No início do desenvolvimento, o mod possuía muito menos funcionalidades (como a restrição de armas/NPCs, heal cooldown) e uma **qualidade de balanceamento** muito inferior ao que possui hoje. 
No entanto, através de testes e feedback de players o mod atingiu um ótimo estado de balanceamento e com maior quantidade de recursos.<br>

Por se tratar de um mod pequeno, eu não utilizei [versionamento semântico](https://www.alura.com.br/artigos/versionamento-semantico-breve-introducao) na nomeação de versões do mod.
Sendo assim, o primeiro dígito é sempre alterado quando grandes mudanças são feitas (major), e o segundo dígito, pequenas mudanças (minor) -> **(vX.X)**.

Documentação do tModloader que serve de grande utilidade no desenvolvimento:
https://docs.tmodloader.net/docs/stable/index.html <br>

<h2>Como instalar o mod:</h2>

Para adicionar o mod ao seu jogo você tem as seguintes alternativas (considerando que seu jogo está em inglês):
- Abrir o [Terraria](https://store.steampowered.com/app/105600/Terraria/) com o tModLoader e procurar pelo nome do mod através da lista de mods:
Menu principal do jogo > Workshop > Download Mods > (Buscar o nome do mod e se inscrever).<br>
- Você também pode apenas procurá-lo diretamente na [Workshop do Terraria](https://steamcommunity.com/app/105600/workshop/), na Steam.

**Link do mod na workshop da Steam com todas as notas de alterações (patch-notes): https://tinyurl.com/mwpakyeh**<br>

<h2>Informações para jogadores de Terraria - Vampire Claw:</h2>

O item possui os seguintes [stats](https://terraria.fandom.com/wiki/Player_stats):
- Adiciona 5% de roubo de vida (não se aplica em PvP).<br>
- Adiciona 4% de velocidade de ataque em armas da [classe Melee](https://terraria.fandom.com/wiki/Melee_weapons).<br>
Obs.: *Roubo de vida é a quantidade de cura recebida com base no dano causado a inimigos.*

<h4>Informações de balanceamento:</h4>

- A quantidade máxima curada é limitada nos seguintes casos:
  
  > Normal hit:                    3HP<br>
  > Critical hit (Acerto crítico): 4HP<br>
  > Passiva ativada (Near Death):  7HP (independente de ter sido acerto crítico)<br>
- **Existe um "cooldown" de 0.260ms entre curas**, esse cooldown é reduzido pela metade (0.130ms) quando o jogador está com a passiva Near Death ativada.
- A passiva "Near Death" é ativada quando a vida atual do jogador é igual ou inferior a 15% da sua vida máxima. Além disso:

  > O jogador recebe um bônus de 4% de roubo de vida e 6% de velocidade de ataque (Melee), totalizando 9% de roubo de vida e 10% de
  >  velocidade de ataque (2.5x o valor básico).

- **O efeito de roubo de vida desse acessório não é aplicado nos seguintes casos**:

  > - O jogador está equipado com o set de Spectre e está aplicando dano mágico.<br>
  >*Obs.: O [armor set de Spectre](https://terraria.fandom.com/wiki/Spectre_armor) é uma armadura para magos (nativa do Terraria) que já possui efeitos de roubo de vida com dano mágico.*<br>
  > - O jogador está utilizando a [Vampire Knives](https://terraria.fandom.com/wiki/Vampire_Knives) ou [Life Drain](https://terraria.fandom.com/wiki/Life_Drain), ambas armas que já aplicam roubo de vida.
  > - O jogador atingiu NPCs que são considerados inofensivos/estruturas do jogo, por ex.: [Target Dummy](https://terraria.fandom.com/wiki/Target_Dummy) e os [pilares pré-moonlord](https://terraria.fandom.com/wiki/Celestial_Pillars).

<h4>Como conseguir o item:</h4>

  - O item não possui crafting e é obtido exclusivamente através de Loot Drop:
    > [Herpling](https://terraria.fandom.com/wiki/Herpling) - Chance: 0.5%.<br>
    > [Corruptor](https://terraria.fandom.com/wiki/Corruptor) - Chance: 0.5%.

Ambos podem ser encontrados no bioma [Crimson](https://terraria.fandom.com/wiki/The_Crimson) e [Corruption](https://terraria.fandom.com/wiki/The_Corruption), respectivamente.

<h4>Informações adicionais:</h4>

Todos os valores de balanceamento foram definidos com base no feedback de players que usaram o mod e diversos testes feitos por mim.<br><br>
**A partir da atualização 4.0 desse mod**, o jogador pode alterar esses valores (padrões) a seu gosto (que foram criados para manter uma gameplay balanceada) através do menu principal, em "Manage Mods" e clicando na engrenagem.

Ícone/modelo criado pelo meu amigo: Nícolas Fernandes.
