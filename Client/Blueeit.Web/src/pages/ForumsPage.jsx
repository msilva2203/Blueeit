import { useEffect, useState } from 'react';

import ThreadList from "../components/ThreadList";
import Block from "../components/Block";
import BlockItem from "../components/BlockItem";
import BlockHeader from "../components/BlockHeader";
import BlockFooter from "../components/BlockFooter";
import ForumNode from "../components/ForumNode";
import BlockSpacer from "../components/BlockSpacer";
import PageLayout from '../components/PageLayout';
import BodyHeader from '../components/BodyHeader';
import OnlineMembersPanel from '../components/panels/OnlineMembersPanel';
import StatsPanel from '../components/panels/StatsPanel';

const API_URL = "http://localhost:5230/api/v1"

export default function ForumsPage() {
    const [forums, setForums] = useState([]);
    const [page, setPage] = useState(1);
    const [totalPages, setTotalPages] = useState(1);
    const [loading, setLoading] = useState(true);

    const pageSize = 20;

    const defaultForums = [
        {
            id: 1,
            title: "Instructions and Schedule"
        },
        {
            id: 2,
            title: "Changelog"
        },
        {
            id: 3,
            title: "Bug Reports"
        },
        {
            id: 4,
            title: "Community 1"
        },
        {
            id: 5,
            title: "Community 2"
        },
        {
            id: 6,
            title: "Community 3"
        },
        {
            id: 7,
            title: "Community 4"
        }
    ];

    useEffect(() => {
        async function fetchForums() {
            setLoading(true);

            try {
                const response = await fetch(
                    `${API_URL}/forums?page=${page}&pageSize=${pageSize}`
                );

                if (!response.ok) {
                    throw new Error("Failed to fetch forums!");
                }

                const data = await response.json();

                setForums(data.items);
                setTotalPages(data.totalPages);
            } catch (error) {
                console.error(error);
                setForums(defaultForums);
            } finally {
                setLoading(false);
            }
        }

        fetchForums();
    }, [page])

    return (
        <>
            <BodyHeader title="Forums" />
            <PageLayout sidebar={<><OnlineMembersPanel /><BlockSpacer /><StatsPanel /></>}>
                <Block>
                    <BlockHeader>
                        General
                    </BlockHeader>
                    {loading ? (
                        <>
                        </>
                    ) : (
                        <div>
                            {forums.map((forum) => (
                            <BlockItem>
                                <ForumNode forum={forum} />
                            </BlockItem>
                            ))}
                        </div>
                    )}
                </Block>
            </PageLayout>
        </>
    );
}