import { QueryParameters } from 'src/app/shared/query-parameters';


/**
 * Post查询参数类
 */
export class PostParameters extends QueryParameters {
    title?: string

    constructor(init?: Partial<PostParameters>) {
        super(init);
        Object.assign(this, init);
    }
}