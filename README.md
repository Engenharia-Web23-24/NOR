# NOR

Proposta de resolução da prova prática referente à componente prática da avaliação por exame da época normal.
ATENÇÃO:
O projeto disponibilizado tem um "bloqueio" na renderização nos links na view da liastegm por força da questão 5 do teste.
Para poder renderiazar esses links é necessário comentar ocódigo que implmeenta esse bloqueio.

@if (Context.Session.Keys.Contains("estado") && Context.Session.GetString("estado") == "editar")
