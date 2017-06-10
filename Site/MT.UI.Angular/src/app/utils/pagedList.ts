import { QueryList } from "@angular/core/core";

export class PagedList<TEntity>{
    qtdeItens:number;
    qtdeItensPorPagina:number;
    qtdePaginas:number;
    itens : QueryList<TEntity>;
}