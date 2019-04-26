/**
 * 查询参数父类
 */
export abstract class QueryParameters {

    pageIndex?:number;
    pageSize?:number;
    fields?:string;
    orderBy?:string;


    constructor(init?: Partial<QueryParameters>) {
        Object.assign(this, init)
    }
}