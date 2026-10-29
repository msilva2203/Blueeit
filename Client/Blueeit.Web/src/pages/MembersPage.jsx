import { useState, useEffect } from "react";
import { useSearchParams } from "react-router-dom";

import { usePaginatedState } from "../hooks/usePaginatedState";

import Block from "../components/Block";
import BlockContent from "../components/BlockContent";
import BodyHeader from "../components/BodyHeader";
import BlockOuterBottom from "../components/BlockOuterBottom";
import Pagination from "../components/Pagination";
import PageLayout from "../components/PageLayout";
import MembersSideNavPanel from "../components/panels/MembersSidenavPanel";
import OnlineMembersPanel from "../components/panels/OnlineMembersPanel";
import MemberNodeList from "../components/MemberNodeList";
import ErrorBlock from "../components/ErrorBlock";

const API_URL = "http://localhost:5230/api/v1";

export default function MembersPage() {
    const [searchParams] = useSearchParams();

    const users = usePaginatedState({
        initialPage: 1
    });

    const key = searchParams.get("key") ?? "none";

    const keyMappings = {
        "none": "none",
        "newest": "newest",
        "oldest": "oldest",
        "most-messages": "mostMessages",
        "todays-birthdays": "none",
        "staff": "none"
    };

    const keyIndexMappings = {
        "none": 1,
        "newest": 2,
        "oldest": 3,
        "most-messages": 4,
        "todays-birthdays": 5,
        "staff": 6
    };

    const keyTitleMappings = {
        "none": "Members",
        "newest": "Newest Members",
        "oldest": "Oldest Members",
        "most-messages": "Most Messages",
        "todays-birthdays": "Today's Birthdays",
        "staff": "Staff"
    };

    const actualKey = keyMappings[key] ?? "none";
    const actualKeyIndex = keyIndexMappings[key] ?? 0;
    const actualKeyTitle = keyTitleMappings[key] ?? "Members";

    const pageSize = 20;

    useEffect(() => {
        async function fetchUsers() {
            users.setIsLoading(true);

            try {
                const response = await fetch(
                    `${API_URL}/users?key=${actualKey}&page=${users.page}&pageSize=${pageSize}`
                );

                if (!response.ok) {
                    throw new Error("Failed to fetch users!");
                }

                const data = await response.json();

                users.setData(data);
            } catch (error) {
                console.error(error);

                users.setError(error);
            } finally {
                users.setIsLoading(false);
            }
        }

        fetchUsers();
    }, [users.page]);

    function renderMembers() {
        if (users.isLoading) {
            return (
                <small>
                    Loading...
                </small>
            );
        }

        if (users.error != null) {
            return (
                <ErrorBlock>
                    <BlockContent>
                        <small className="blueeit-error">
                            Unable to load members.
                        </small>
                    </BlockContent>
                </ErrorBlock>
            );
        }

        return (
            <>
                <Block>
                    <MemberNodeList members={users.items}/>
                </Block>
                { users.totalPages > 1 ? (
                    <BlockOuterBottom>
                        <Pagination currentPage={users.page} totalPages={users.totalPages} setPage={users.setPage} />
                    </BlockOuterBottom>
                ) : (
                    <>
                    </>
                )}
            </>
        );
    }

    return (
        <>
            <BodyHeader title={actualKeyTitle} />
            <PageLayout sidebar={<><OnlineMembersPanel/></>} sidenav={<><MembersSideNavPanel selectionIndex={actualKeyIndex}/></>}>
                {renderMembers()}
            </PageLayout>
        </>
    );
}