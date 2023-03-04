export type Login = {
  login: string;
  password: string;
};

export type Token = {
  token: string;
  id: string;
  role: string;
};

export type User = {
  id?: string;
  login: string;
  password: string;
  role: string;
  name: string;
  lastname: string;
  email: string;
  phone?: string;
  profilePicture?: string;
  fileMime?: string;
  description?: string;
  createdAt?: string;
  modifiedAt?: string;
  deleted?: boolean;
  banned?: boolean;
};

export type Group = {
  id?: string;
  name: string;
  visibility: string;
  iconPicture?: string;
  backgroundPicture?: string;
  description?: string;
  userId: string;
  createdAt?: string;
  modifiedAt?: string;
  deleted?: boolean;
  banned?: boolean;
};

export type GroupPost = {
  groupId: string;
  postId: string;
};
export type GroupUser = {
  groupId: string;
  userId: string;
  isAdmin: boolean;
};

export type Post = {
  id?: string;
  title: string;
  content: string;
  picture?: string;
  type?: string;
  userId?: string;
  createdAt?: string;
  modifiedAt?: string;
  deleted?: boolean;
  banned?: boolean;
  comments?: Comment[];
  newComment?: string;
  user?: User;
  group?: Group;
};

export type Comment = {
  id?: string;
  postId: string;
  parentId: string;
  content: string;
  userId?: string;
  createdAt?: string;
  modifiedAt?: string;
  deleted?: boolean;
  user?: User;
};

export type ChangePassword = {
  oldPassword: string;
  newPassword: string;
};
