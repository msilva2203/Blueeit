import { useParams } from "react-router-dom";

import Breadcrumbs from "../components/Breadcrumbs";
import PageLayout from "../components/PageLayout";
import OnlineMembersPanel from "../components/panels/OnlineMembersPanel";
import MemberBanner from "../components/MemberBanner";
import BlockSpacer from "../components/BlockSpacer";
import PostList from "../components/PostList";

export default function MemberPage() {
    const { id } = useParams();

    const posts = [
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
                <MemberBanner />
                <BlockSpacer />
                <PostList posts={posts} />
            </PageLayout>
            <Breadcrumbs items={breadcrumbs} />
        </>
    );
}