import { useEffect, useState } from 'react';

import { useParams } from "react-router-dom";
import Block from "../components/Block";
import BlockHeader from "../components/BlockHeader";
import BlockSpacer from "../components/BlockSpacer";
import Post from "../components/Post";
import Pagination from '../components/Pagination';
import PostList from '../components/PostList';
import BlockOuterTop from '../components/BlockOuterTop';
import BlockOuterBottom from '../components/BlockOuterBottom';
import BodyHeader from '../components/BodyHeader';
import PageLayout from '../components/PageLayout';
import Breadcrumbs from '../components/Breadcrumbs';
import OnlineMembersPanel from '../components/panels/OnlineMembersPanel';

const API_URL = "http://localhost:5230/api/v1";

export default function ThreadPage() {
    const { id } = useParams();

    const [posts, setPosts] = useState([]);
    const [page, setPage] = useState(1);
    const [totalPages, setTotalPages] = useState(1);
    const [loading, setLoading] = useState(true);

    const pageSize = 20;

    const defaultPost = {
        "id": 1
    }

    useEffect(() => {
        async function fetchPosts() {
            setLoading(true);

            try {
                const response = await fetch(
                    `${API_URL}/threads/${id}/posts?page=${page}&pageSize=${pageSize}`
                );

                if (!response.ok) {
                    throw new Error("Failed to fetch posts!");
                }

                const data = await response.json();

                setPosts(data.items);
                setTotalPages(data.totalPages);
            } catch (error) {
                console.error(error);
            } finally {
                setLoading(false);
            }
        }

        fetchPosts();
    }, [page])

    const breadcrumbs = [
        {
            "name": "Forums",
            "url": "/forums"
        },
        {
            "name": "Bug Reports",
            "url": "/forums/1"
        }
    ]
    
    return (
        <>
            <Breadcrumbs items={breadcrumbs}/>
            <BodyHeader title="Bad Matchmaking" />
            <PageLayout sidebar={<><OnlineMembersPanel /></>}>
                {loading ? (
                    <>
                    </>
                ) : (
                    <>
                        <BlockOuterTop>
                            <Pagination currentPage={page} totalPages={totalPages} setPage={setPage} />
                        </BlockOuterTop>

                        <PostList posts={posts} />

                        <BlockOuterBottom>
                            <Pagination currentPage={page} totalPages={totalPages} setPage={setPage} />
                        </BlockOuterBottom>

                    </>
                )}
            </PageLayout>
            <BlockSpacer />
            <Breadcrumbs items={breadcrumbs} />
        </>
    );
}