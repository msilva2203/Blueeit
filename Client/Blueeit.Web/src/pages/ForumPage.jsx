import { useEffect, useState } from 'react';

import { useParams } from "react-router-dom";
import Block from "../components/Block";
import BlockHeader from "../components/BlockHeader";
import BlockItem from "../components/BlockItem";
import BlockSpacer from "../components/BlockSpacer";
import ForumNode from "../components/ForumNode";
import ThreadNode from '../components/ThreadNode';
import BlockOuterTop from '../components/BlockOuterTop';
import Pagination from '../components/Pagination';
import BlockOuterBottom from '../components/BlockOuterBottom';
import BodyHeader from '../components/BodyHeader';
import PageLayout from '../components/PageLayout';
import Breadcrumbs from '../components/Breadcrumbs';
import OnlineMembersPanel from '../components/panels/OnlineMembersPanel';

const API_URL = "http://localhost:5230/api/v1";

export default function ForumPage() {
    const { id } = useParams();

    const [subforums, setSubforums] = useState([]);
    const [subforumsPage, setSubforumsPage] = useState(1);
    const [subforumsTotalPages, setSubforumsTotalPages] = useState(1);
    const [subforumsLoading, setSubforumsLoading] = useState(true);

    const [threads, setThreads] = useState([]);
    const [threadsPage, setThreadsPage] = useState(1);
    const [threadsTotalPages, setThreadsTotalPages] = useState(1);
    const [threadsLoading, setThreadsLoading] = useState(true);

    const subforumsPageSize = 5;
    const threadsPageSize = 20;

    const defaultSubforums = [
        {
            id: 1,
            title: "Instructions and Schedule"
        }
    ];

    const defaultThreads = [
        {
            id: 1,
            title: "Terrible Matchmaking"
        },
        {
            id: 2,
            title: "Terrible Matchmaking"
        },
        {
            id: 3,
            title: "Terrible Matchmaking"
        },
        {
            id: 4,
            title: "Terrible Matchmaking"
        }
    ];

    useEffect(() => {
        async function fetchSubforums() {
            setSubforumsLoading(true);

            try {
                const response = await fetch(
                    `${API_URL}/forums/${id}/subforums?page=${subforumsPage}&pageSize=${subforumsPageSize}`
                );

                if (!response.ok) {
                    throw new Error("Failed to fetch forums!");
                }

                const data = await response.json();

                setSubforums(data.items);
                setSubforumsTotalPages(data.totalPages);
            } catch (error) {
                console.error(error);
                setSubforums(defaultSubforums);
            } finally {
                setSubforumsLoading(false);
            }
        }

        fetchSubforums();
    }, [subforumsPage])

    useEffect(() => {
        async function fetchThreads() {
            setThreadsLoading(true);

            try {
                const response = await fetch(
                    `${API_URL}/forums/${id}/threads?page=${threadsPage}&pageSize=${threadsPageSize}`
                );

                if (!response.ok) {
                    throw new Error("Failed to fetch threads!");
                }

                const data = await response.json();

                setThreads(data.items);
                setThreadsTotalPages(data.totalPages);
            } catch (error) {
                console.error(error);
                setThreads(defaultThreads);
            } finally {
                setThreadsLoading(false);
            }
        }

        fetchThreads();
    }, [threadsPage])

    const breadcrumbs = [
        {
            "name": "Forums",
            "url": "/forums"
        }
    ];

    return (
        <>
            <Breadcrumbs items={breadcrumbs} />
            <BodyHeader title="Bug Reports" />
            <PageLayout sidebar={<OnlineMembersPanel />}>
                {subforumsLoading ? (
                    <>
                    </>
                ) : (
                    <>
                        <BlockOuterTop>
                            <Pagination currentPage={subforumsPage} totalPages={subforumsTotalPages} setPage={setSubforumsPage} />
                        </BlockOuterTop>
                        <Block>
                            {subforums.map((subforum) => (
                                <BlockItem>
                                    <ForumNode forum={subforum} />
                                </BlockItem>
                            ))}
                        </Block>
                        <BlockSpacer />
                    </>
                )}

                <BlockOuterTop>
                    <Pagination currentPage={threadsPage} totalPages={threadsTotalPages} setPage={setThreadsPage} />
                </BlockOuterTop>
                <Block>
                    <BlockHeader>
                        {id}
                    </BlockHeader>
                    {threadsLoading ? (
                        <>
                        </>
                    ) : (
                        <>
                            {threads.map((thread) => (
                                <BlockItem>
                                    <ThreadNode thread={thread} />
                                </BlockItem>
                            ))}
                        </>
                    )}
                </Block>
                <BlockOuterBottom>
                    <Pagination currentPage={threadsPage} totalPages={threadsTotalPages} setPage={setThreadsPage} />
                </BlockOuterBottom>
            </PageLayout>
            <BlockSpacer />
            <Breadcrumbs items={breadcrumbs} />
        </>
    );
}