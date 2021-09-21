import { Byte } from "@angular/compiler/src/util";
import { Comment } from "./comment";

export interface Post {
    id: number,
    userId: number,
    image: Byte,
    created: Date,
    title: string,
    body: string, 
    username: string,
    comments: Comment[],
}