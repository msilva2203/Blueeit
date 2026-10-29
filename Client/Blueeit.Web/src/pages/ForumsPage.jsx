import { useEffect, useState } from 'react';

import { usePaginatedState } from '../hooks/usePaginatedState';

import { getForums } from '../api/forums';

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
import ForumNodeList from '../components/ForumNodeList';
import BlockContent from '../components/BlockContent';
import ErrorBlock from '../components/ErrorBlock';

const API_URL = "http://localhost:5230/api/v1";

export default function ForumsPage() {
    const forums = usePaginatedState({
        initialPage: 1
    });

    const pageSize = 20;

    useEffect(() => {
        async function fetchForums() {
            forums.setIsLoading(true);

            try {
                const data = await getForums(forums.page, pageSize);

                forums.setData(data);
            } catch (error) {
                console.error(error);

                forums.setError(error);
            } finally {
                forums.setIsLoading(false);
            }
        }

        fetchForums();
    }, [forums.page])

    function renderForums() {
        if (forums.isLoading) {
            return (
                <BlockContent>
                    <small>
                        Loading...
                    </small>
                </BlockContent>
                
            );
        }

        if (forums.error != null) {
            return (
                <BlockContent>
                    <ErrorBlock>
                        <BlockContent>
                            <small className="blueeit-error">
                                Unable to load forums.
                            </small>
                        </BlockContent>
                    </ErrorBlock>
                </BlockContent>
            );
        }

        return (
            <>
                <ForumNodeList forums={forums.items} />
            </>
        );
    }

    return (
        <>
            <BodyHeader title="Forums" />
            <PageLayout sidebar={<><OnlineMembersPanel /><BlockSpacer /><StatsPanel /></>}>
                <Block>
                    <BlockHeader>
                        General
                    </BlockHeader>
                    {renderForums()}
                </Block>
            </PageLayout>
        </>
    );
}