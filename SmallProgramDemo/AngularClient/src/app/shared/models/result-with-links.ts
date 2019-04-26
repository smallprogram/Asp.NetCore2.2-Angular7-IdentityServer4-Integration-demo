import { Link } from './link';

export class ResultWithLinks<T> {
    links: Link[];
    values: T[];
}
