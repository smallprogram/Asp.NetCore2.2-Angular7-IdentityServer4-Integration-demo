import { Entity } from 'src/app/shared/models/entity';

export class Post extends Entity {
    title:string;
    body:string;
    author:string;
    updateTime:Date;
    remark:string;
}
