import PageLayout from "../components/PageLayout";
import OnlineMembersPanel from "../components/panels/OnlineMembersPanel";

export default function MembersPage() {
    return (
        <>
            <PageLayout sidebar={<><OnlineMembersPanel/></>}>
                <h1>Members</h1>
                <p1>Welcome to the Blueeit Members page!</p1>
            </PageLayout>
        </>
    );
}