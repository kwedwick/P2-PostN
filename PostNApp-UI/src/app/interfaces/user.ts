import { Post } from "./post";
import { Comment } from "./comment";
import { IFriend } from "./friend";

export interface User {
    id: number,
    firstName: string,
    lastName: string,
    email: string,
    username: string,
    password: string,
    aboutMe: string,
    state: string,
    country: string,
    role: string,
    phoneNumber: string,
    doB: Date,
    comments: Comment[],
    posts: Post[],
    friends: IFriend[]
}