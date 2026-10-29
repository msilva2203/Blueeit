import { useState, useEffect } from "react";
import { useParams } from "react-router-dom";

import Breadcrumbs from "../components/Breadcrumbs";
import PageLayout from "../components/PageLayout";
import OnlineMembersPanel from "../components/panels/OnlineMembersPanel";
import MemberBanner from "../components/MemberBanner";
import BlockSpacer from "../components/BlockSpacer";
import PostList from "../components/PostList";
import ProfilePostList from "../components/ProfilePostList";
import BlockOuterBottom from "../components/BlockOuterBottom";
import Pagination from "../components/Pagination";

const API_URL = "http://localhost:5230/api/v1";

export default function MemberPage() {
    const { id } = useParams();

    const [user, setUser] = useState({});
    const [userLoading, setUserLoading] = useState(true);

    const [posts, setPosts] = useState([]);
    const [postsPage, setPostsPage] = useState(1);
    const [postsTotalPages, setPostsTotalPages] = useState(1);
    const [postsLoading, setPostsLoading] = useState(true);

    const postsPageSize = 20;

    useEffect(() => {
        async function fetchUser() {
            setUserLoading(true);

            try {
                const response = await fetch(
                    `${API_URL}/users/${id}`
                );

                if (!response.ok) {
                    throw new Error("Failed to fetch user!");
                }

                const data = await response.json();

                setUser(data);
            } catch (error) {
                console.error(error)
            } finally {
                setUserLoading(false);
            }
        }

        fetchUser();
    }, [])

    useEffect(() => {
        async function fetchProfilePosts() {
            setPostsLoading(true);

            try {
                const response = await fetch(
                    `${API_URL}/users/${id}/profile-posts?page=${postsPage}&pageSize=${postsPageSize}`
                );

                if (!response.ok) {
                    throw new Error("Failed to fetch profile posts!");
                }

                const data = await response.json();

                setPosts(data.items);
                setPostsTotalPages(data.totalPages);
            } catch (error) {
                console.error(error);
                setPosts(defaultPosts);
            } finally {
                setPostsLoading(false);
            }
        }

        fetchProfilePosts();
    }, [postsPage])

    const defaultPosts = [
        {
            "id": 1,
            "content": "Hello"
        },
        {
            "id": 1,
            "content": "Hello"
        },
        {
            "id": 1,
            "content": "Hello"
        },
        {
            "id": 1,
            "content": "Hello"
        },
        {
            "id": 1,
            "content": "Hello"
        },
        {
            "id": 1,
            "content": "Hello"
        },
        {
            "id": 1,
            "content": "Hello"
        },
        {
            "id": 1,
            "content": "Hello"
        },
        {
            "id": 1,
            "content": "Hello"
        },
        {
            "id": 1,
            "content": "Hello"
        },
        {
            "id": 1,
            "content": "Hello"
        },
        {
            "id": 1,
            "content": "Hello"
        },
        {
            "id": 1,
            "content": "Hello"
        },
        {
            "id": 1,
            "content": "Hello"
        },
        {
            "id": 1,
            "content": "Hello"
        },
        {
            "id": 1,
            "content": "Hello"
        },
        {
            "id": 1,
            "content": "Hello"
        },
        {
            "id": 1,
            "content": "Hello"
        },
        {
            "id": 1,
            "content": "Hello"
        },
    ];

    const breadcrumbs = [
        {
            "name": "Members",
            "url": "/members"
        }
    ];

    return (
        <>
            <Breadcrumbs items={breadcrumbs} />
            <PageLayout sidebar={<><OnlineMembersPanel/></>}>
                <MemberBanner user={user}/>
                <BlockSpacer />
                { postsLoading ? (
                    <></>
                ) : (
                    <>
                        <ProfilePostList posts={posts} />
                        { postsTotalPages > 1 ? (
                            <>
                                <BlockOuterBottom>
                                    <Pagination currentPage={postsPage} totalPages={postsTotalPages} setPage={setPostsPage} />
                                </BlockOuterBottom>
                            </>
                        ) : (
                            <>
                            </>
                        )}
                    </>
                ) }
            </PageLayout>
            <BlockSpacer />
            <Breadcrumbs items={breadcrumbs} />
        </>
    );
}